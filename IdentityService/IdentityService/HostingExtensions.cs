using System.Reflection;
using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using FastEndpoints;
using IdentityService.Commands;
using IdentityService.Communicators;
using IdentityService.Data;
using IdentityService.Infrastructure;
using IdentityService.Messages;
using IdentityService.Models;
using IdentityService.Persistence;
using IdentityService.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;

namespace IdentityService;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        //Fast endpoints
        //builder.Services.AddFastEndpoints();
        builder.Services.AddControllers();
        
        
        //Postgres legacy timestamp
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        //DI
        builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder.Services
            .AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
        builder.Services.AddSingleton<IRabbitCommunicator, RabbitCommunicator>();
        builder.Services.AddSingleton<IRabbitMessageHandler, RabbitMessageHandler>();
        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var cfg = builder.Configuration.GetConnectionString("RedisConnection");
            return ConnectionMultiplexer.Connect(cfg);
        });
        builder.Services.AddSingleton<IRedisRepository, RedisRepository>();
        
        //MediatR
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateShortSessionCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(ValidateShortSessionQueryHandler).Assembly);
        });
        
        builder.Services.AddHttpClient("LoyaltyService", client =>
        {
            client.BaseAddress = new Uri("https://localhost:8004/");
        });
        
        builder.Services.AddHttpClient("OtpService", client =>
        {
            client.BaseAddress = new Uri("https://localhost:8001/");
        });
        
        //Service registrations
        
        builder.Services.AddRazorPages(o =>
        {
            o.Conventions.AllowAnonymousToPage("/Account/Register");
            o.Conventions.AllowAnonymousToPage("/Account/ConfirmEmail");
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));


        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                //options.Authentication.CookieLifetime = new TimeSpan(90, 0, 0, 0); 
                
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                
                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddConfigurationStore(o => 
                o.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                    psql => psql.MigrationsAssembly(migrationsAssembly)))
            .AddOperationalStore(o => 
                o.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                    x => x.MigrationsAssembly(migrationsAssembly)))
            /*
             Configuration and operational data are persisted and populated by migration at startup.
             
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            */
            .AddAspNetIdentity<ApplicationUser>();

        builder.Services.AddAuthentication();
            /*
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });*/


        var app = builder.Build();

        //Populate DB with data in Config.cs
        MigrateConfigContent(app);

        //Register customer to RMQ exchange
        //RegisterRabbitConsumerToExchange(app, "mta_exchange");
        
        return app;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();
        app.UseAuthorization();
        
        
        //app.UseFastEndpoints();
        
        app.MapRazorPages()
            .RequireAuthorization();

        app.MapControllers();

        return app;
    }

    private static void MigrateConfigContent(IApplicationBuilder app)
    {
        

        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        context.Database.Migrate();
            
        if (!context.Clients.Any())
        {
            foreach (var client in Config.Clients)
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.IdentityResources)
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in Config.ApiScopes)
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
    }
    
    static void RegisterRabbitConsumerToQueue(IApplicationBuilder app, string queueName)
    {
        using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var communicator = scope.ServiceProvider.GetRequiredService<IRabbitCommunicator>();
            communicator.ReceiveMessageFromQueue(queueName);
        }
    }

    static void RegisterRabbitConsumerToExchange(IApplicationBuilder app, string exchangeName)
    {
        using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var communicator = scope.ServiceProvider.GetRequiredService<IRabbitCommunicator>();
            communicator.ReceiveMessageFromExchange(exchangeName);
        }
    }
}
