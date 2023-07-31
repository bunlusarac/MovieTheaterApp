using System.Reflection;
using Hangfire;
using Hangfire.Mongo;
using Hellang.Middleware.ProblemDetails;
using Microsoft.OpenApi.Models;
using MovieService.Domain.Exceptions;
using MovieService.Persistence.Exceptions;
using MovieService.Persistence.Repositories;
using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Inject repository
var movieConnectionString = builder.Configuration.GetConnectionString("MovieDb");
var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireDb");
const string dbName = "moviedb";

builder.Services.AddScoped<MovieRepository>(provider => new MovieRepository(movieConnectionString, dbName));

// Register Hangfire
builder.Services.AddHangfire(cfg =>
{
    // Current db does not support change stream (not a replica set)
    // For instant (almost) handling of enqueued jobs, CheckQueuedJobsStrategy is set to TailNotificationsCollection
    cfg.UseMongoStorage(hangfireConnectionString, new MongoStorageOptions
    {
        CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
    });
});

// Map thrown exceptions to problem details 
builder.Services.AddProblemDetails((ProblemDetailsOptions cfg) =>
{
    cfg.IncludeExceptionDetails = (context, exception) =>
        builder.Environment.IsDevelopment() || builder.Environment.IsStaging();

    cfg.Map<MovieDomainException>((context, exception) =>
    {
        exception.ProblemDetails.Instance = context.Request.Path.ToString();
        return exception.ProblemDetails;
    });
    
    cfg.Map<MoviePersistenceException>((context, exception) =>
    {
        exception.ProblemDetails.Instance = context.Request.Path.ToString();
        return exception.ProblemDetails;
    });
});
    
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MovieService",
        Description = "API for managing movie metadata and rating.",
        Version = "v1",
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    cfg.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();
app.UseHangfireServer();

app.MapControllers();

app.Run();