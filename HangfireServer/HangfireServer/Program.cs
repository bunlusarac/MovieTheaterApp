using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HangfireDb");

//GlobalConfiguration.Configuration.UsePostgreSqlStorage(connectionString);

builder.Services.AddHangfire(cfg =>
{
    cfg.UsePostgreSqlStorage(connectionString);
});

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseHangfireDashboard();

app.MapGet("/", () => "Hello World!");


app.Run();