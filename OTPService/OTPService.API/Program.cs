using System.Reflection;
using Microsoft.EntityFrameworkCore;

using OTPService.Application.Commands;
using OTPService.Application.Communicators;
using OTPService.Application.Persistence;
using OTPService.Infrastructure.Communicators;
using OTPService.Infrastructure.Messages;
using OTPService.Persistence.Contexts;
using OTPService.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IOtpUserRepository, OtpUserRepository>();
builder.Services.AddTransient<IEmailServiceCommunicator, EmailServiceCommunicator>();
builder.Services.AddTransient<ISmsServiceCommunicator, SmsServiceCommunicator>();

builder.Services.AddSingleton<IIdentityServiceCommunicator, IdentityServiceCommunicator>();
builder.Services.AddSingleton<IRabbitMessageHandler, RabbitMessageHandler>();
builder.Services.AddSingleton<IRabbitCommunicator, RabbitCommunicator>();
//builder.Services.AddHostedService<RabbitSubscriber>();

builder.Services.AddHttpClient("IdentityService", client =>
{
    client.BaseAddress = new Uri("https://localhost:8006/");
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(IssueOtpCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RegisterOtpUserCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateOtpUserMfaStatusCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(ValidateOtpCommandHandler).Assembly);
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<OtpDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("OtpDbContext")));

var app = builder.Build();

RegisterRabbitConsumerToExchange(app, "mta_exchange");

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