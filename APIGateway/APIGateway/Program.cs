using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();

var authenticationProviderKey = "IdentityApiKey";

builder.Services.AddAuthentication().AddJwtBearer(authenticationProviderKey, x =>
{
    x.Authority = "https://localhost:8001";
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseOcelot();
app.Run();