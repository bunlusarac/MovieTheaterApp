using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using APIGateway.Communicators;
using APIGateway.DelegatingHandlers;
using APIGateway.Persistence;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");
    return ConnectionMultiplexer.Connect(redisConnectionString);
});
builder.Services.AddSingleton<IRedisRepository, RedisRepository>();

builder.Services.AddSingleton<IdentityServiceCommunicator>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer(o =>
{
    o.Authority = "https://localhost:8006";
    o.TokenValidationParameters.ValidateAudience = false;
    o.MapInboundClaims = false;
});

builder.Services.AddHttpClient("IdentityService", client =>
{
    client.BaseAddress = new Uri("https://localhost:8006/");
    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
});

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("gateway", p =>
    {
        p.RequireAuthenticatedUser();
        p.RequireClaim("scope", "gateway");
    });
});

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services
    .AddOcelot()
    .AddDelegatingHandler<ShortSessionAuthenticationHandler>()
    .AddDelegatingHandler<TokenBasedRateLimitingHandler>();

builder.Services.AddLogging();
/*
builder.Services.AddKeycloakAuthentication(new KeycloakAuthenticationOptions
{
    AuthServerUrl = "http://localhost:8006",
    Realm = "mta-realm",
    Resource = "mta-client",
});


builder.Services.AddKeycloakAuthentication(builder.Configuration);

*/

/*
var cfg = new OcelotPipelineConfiguration()
{
    PreQueryStringBuilderMiddleware = async (ctx, next) =>
    {
        await next.Invoke();
    }
};
*/
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.UseRouting();
app.MapControllers().RequireAuthorization("gateway");

app.UseOcelot();

app.Run();