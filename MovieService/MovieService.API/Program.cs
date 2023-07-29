using System.Reflection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MovieService.Domain.Exceptions;
using MovieService.Persistence.Exceptions;
using MovieService.Persistence.Repositories;
using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Inject repository
var connectionString = builder.Configuration.GetConnectionString("MovieDb");
var dbName = "moviedb";

builder.Services.AddScoped<MovieRepository>(provider => new MovieRepository(connectionString, dbName));

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

app.MapControllers();

app.Run();