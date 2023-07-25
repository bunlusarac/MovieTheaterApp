using System.ComponentModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using VenueService.Application.Commands;
using VenueService.Application.Persistence;
using VenueService.Application.Queries;
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
    cfg.RegisterServicesFromAssembly(typeof(CreateSessionCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateVenueCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateTheaterCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(DeleteSessionCommandHandler).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetTheaterSessionsQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AddRowToTheaterLayoutCommand).Assembly);
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<VenueDbContext>(o =>
    o.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("VenueDbContext")));


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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