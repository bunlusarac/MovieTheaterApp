using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(name: "mta-profile", userClaims: new[] {"first_name", "last_name", "dob", "gsm", "mfa_enabled", "student_exp"})
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("gateway"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            /*
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            */
            new Client
            {
                ClientId = "mta-client",
                ClientSecrets = { new Secret("secret".Sha256())},
                
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                
                //Post-login redirection
                RedirectUris = { "https://localhost:8007/signin-oidc"},

                //Post-logout redirection
                PostLogoutRedirectUris = { "https://localhost:8007/signout-callback-oidc" },
                
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "mta-profile",
                    "gateway"
                },
                
                //Long session = 90 days
                AccessTokenLifetime = 60 * 60 * 24 * 90,
            }
        };
}