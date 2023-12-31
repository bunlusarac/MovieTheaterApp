using System.IdentityModel.Tokens.Jwt;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();

builder.Services.AddBff().AddRemoteApis();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(o =>
    {
        o.DefaultScheme = "Cookies";
        o.DefaultChallengeScheme = "oidc";
        o.DefaultSignOutScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", o =>
    {
        o.Authority = "https://localhost:8006";
        o.ClientId = "mta-client";
        o.ClientSecret = "secret";
        o.ResponseType = "code";
        o.Scope.Add("gateway");
        o.Scope.Add("mta-profile");
        o.SaveTokens = true;
        o.GetClaimsFromUserInfoEndpoint = true;
        o.ClaimActions.MapAll();
    });

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseBff();
app.UseAuthorization();

app.MapBffManagementEndpoints();
app.MapRemoteBffApiEndpoint("/gateway", "https://localhost:8000").RequireAccessToken(Duende.Bff.TokenType.User);
//app.MapGet("/", () => "Hello World!");

app.Run();