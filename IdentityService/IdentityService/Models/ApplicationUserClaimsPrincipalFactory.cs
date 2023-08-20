using System.Globalization;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityService.Models;

public class ApplicationUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<ApplicationUser>
{
    public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
    {
        
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var id = await base.GenerateClaimsAsync(user);
        
        id.AddClaims(
            new []
            {
                new Claim("first_name", user.FirstName),
                new Claim("last_name", user.LastName),
                new Claim("gsm", user.PhoneNumber),
                new Claim("dob", user.DateOfBirth.ToEpochTime().ToString()),
                new Claim("mfa_enabled", user.TwoFactorEnabled.ToString().ToLowerInvariant()),
                new Claim("is_student", user.IsStudent.ToString().ToLowerInvariant()),
                new Claim("student_exp", user.StudentExpirationDate.ToEpochTime().ToString()),
            });

        return id;
    }
}