using System.Reflection;
using Hellang.Middleware.ProblemDetails;
using LoyaltyService.API.DTOs;
using LoyaltyService.Application.Commands;
using LoyaltyService.Application.Persistence;
using LoyaltyService.Application.Queries;
using LoyaltyService.Domain.Entities;
using LoyaltyService.Domain.Exceptions;
using LoyaltyService.Persistence.Contexts;
using LoyaltyService.Persistence.Exceptions;
using LoyaltyService.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<ILoyaltyCustomerRepository, LoyaltyCustomerRepository>();
builder.Services.AddTransient<ICampaignRepository, CampaignRepository>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateCampaignCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DeleteCampaignCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DepositToWalletCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RedeemCampaignCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RegisterLoyaltyCustomerCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateCampaignCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(WithdrawFromWalletCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetCampaignsQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetCustomerRedeemsQueryHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetCustomerWalletQueryHandler).Assembly);
});

builder.Services.AddProblemDetails(cfg =>
{
    cfg.IncludeExceptionDetails = (ctx, env) => builder.Environment.IsDevelopment() || builder.Environment.IsStaging();

    cfg.Map<LoyaltyDomainException>((context, exception) => new ProblemDetails()
    {
        Detail = exception.ProblemDetails.Detail,
        Status = exception.ProblemDetails.Status,
        Title = exception.ProblemDetails.Title,
        Type = exception.ProblemDetails.Type,
        Instance = context.Request.Path.ToString()
    });
    
    cfg.Map<LoyaltyPersistenceException>( (context, exception) => new ProblemDetails()
    {
        Detail = exception.ProblemDetails.Detail,
        Status = exception.ProblemDetails.Status,
        Title = exception.ProblemDetails.Title,
        Type = exception.ProblemDetails.Type,
        Instance = context.Request.Path.ToString()
    });
});

builder.Services.AddScoped<DbContextBase<Campaign>, CampaignDbContext>();
builder.Services.AddScoped<DbContextBase<LoyaltyCustomer>, LoyaltyCustomerDbContext>();

builder.Services.AddDbContext<LoyaltyCustomerDbContext>(o =>
    o.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("LoyaltyDb")));

builder.Services.AddDbContext<CampaignDbContext>(o =>
    o.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("LoyaltyDb")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LoyaltyService",
        Description = "API for user loyalty points and campaigns management.",
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