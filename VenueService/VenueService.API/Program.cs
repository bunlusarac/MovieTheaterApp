using System.Reflection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VenueService.Application.Commands;
using VenueService.Application.Exceptions;
using VenueService.Application.Persistence;
using VenueService.Application.Queries;
using VenueService.Domain.Exceptions;
using VenueService.Persistence.Contexts;
using VenueService.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IVenueRepository, VenueRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ReleaseSessionSeatCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(ReserveSessionSeatCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetSessionSeatingStateQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetVenueSessionsQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetVenuesQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetTheaterSessionsQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetVenueTheatersQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateSessionCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateVenueCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateTheaterCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DeleteSessionCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AddRowToTheaterLayoutCommand).Assembly);
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddProblemDetails(cfg =>
{
    cfg.IncludeExceptionDetails = (ctx, env) => builder.Environment.IsDevelopment() || builder.Environment.IsStaging();

    cfg.Map<VenueDomainException>((context, exception) => new ProblemDetails()
    {
        Detail = exception.Detail,
        Status = exception.Status,
        Title = exception.Title,
        Type = exception.Type,
        Instance = context.Request.Path.ToString()
    });
    
    cfg.Map<VenueApplicationException>( (context, exception) => new ProblemDetails()
    {
        Detail = exception.Detail,
        Status = exception.Status,
        Title = exception.Title,
        Type = exception.Type,
        Instance = context.Request.Path.ToString()
    });
});

builder.Services.AddDbContext<VenueDbContext>(o =>
    o.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("VenueDbContext")));


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VenueService",
        Description = "API for managing venues, theaters and sessions.",
        Version = "v1",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    cfg.IncludeXmlComments(xmlPath);
    
    xmlFile = $"VenueService.Application.xml";
    xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
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