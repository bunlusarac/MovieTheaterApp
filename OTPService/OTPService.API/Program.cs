using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using OTPService.Application.Commands;
using OTPService.Application.Communicators;
using OTPService.Application.Persistence;
using OTPService.Infrastructure.Communicators;
using OTPService.Persistence.Contexts;
using OTPService.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IOtpUserRepository, OtpUserRepository>();
builder.Services.AddTransient<IEmailServiceCommunicator, EmailServiceCommunicator>();
builder.Services.AddTransient<ISmsServiceCommunicator, SmsServiceCommunicator>();


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