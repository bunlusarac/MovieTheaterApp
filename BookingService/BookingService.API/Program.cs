using BookingService.Application.Commands;
using BookingService.Application.Communicators;
using BookingService.Application.Persistence;
using BookingService.Infrastructure.Communicators;
using BookingService.Infrastructure.Messages;
using BookingService.Persistence.Contexts;
using BookingService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI
builder.Services.AddSingleton<IRabbitMessageHandler, RabbitMessageHandler>();
builder.Services.AddSingleton<IRabbitCommunicator, RabbitCommunicator>();

builder.Services.AddScoped<IVenueServiceCommunicator, VenueServiceCommunicator>();
builder.Services.AddScoped<IMovieServiceCommunicator, MovieServiceCommunicator>();
builder.Services.AddScoped<ILoyaltyServiceCommunicator, LoyaltyServiceCommunicator>();
builder.Services.AddScoped<IIdentityServiceCommunicator, IdentityServiceCommunicator>();

builder.Services.AddScoped<BookingDbContext>();

//builder.Services.AddScoped<IPurchaseTransactionRepository, PurchaseTransactionRepository>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();

/*
builder.Services.AddDbContext<BookingDbContext>(o =>
    o.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("LoyaltyDb")));
*/

builder.Services.AddDbContext<BookingDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("BookingDb")));

//Communicator HTTP clients

builder.Services.AddHttpClient("VenueService", client =>
{
    client.BaseAddress = new Uri("https://localhost:8002/");
});

builder.Services.AddHttpClient("LoyaltyService", client =>
{
    client.BaseAddress = new Uri("https://localhost:8004/");
});

builder.Services.AddHttpClient("IdentityService", client =>
{
    client.BaseAddress = new Uri("https://localhost:8006/");
});

builder.Services.AddHttpClient("MovieService", client =>
{
    client.BaseAddress = new Uri("https://localhost:8003/");
});

//Mediator

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(PurchaseTicketCommandHandler).Assembly);
});

var app = builder.Build();
//RegisterRabbitConsumerToExchange(app, "mta_exchange");

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

void RegisterRabbitConsumerToQueue(IApplicationBuilder app, string queueName)
{
    using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var communicator = scope.ServiceProvider.GetRequiredService<IRabbitCommunicator>();
        communicator.ReceiveMessageFromQueue(queueName);
    }
}

void RegisterRabbitConsumerToExchange(IApplicationBuilder app, string exchangeName)
{
    using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        var communicator = scope.ServiceProvider.GetRequiredService<IRabbitCommunicator>();
        communicator.ReceiveMessageFromExchange(exchangeName);
    }
}