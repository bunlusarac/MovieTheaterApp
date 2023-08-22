using System.IdentityModel.Tokens.Jwt;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

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
        o.Events = new OpenIdConnectEvents
        {
            OnRemoteFailure = (ctx) =>
            {
                ctx.Response.Redirect("/");
                ctx.HandleResponse();
                return Task.CompletedTask;
            }
        };
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
//app.MapRemoteBffApiEndpoint("/identity", "https://localhost:8006").
//app.MapGet("/", () => "Hello World!");
//app.MapBffManagementSilentLoginEndpoints();
app.Run();