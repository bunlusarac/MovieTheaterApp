using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace VenueService.Domain.Utils;

public static class ConcurrencyTokenHelper
{
    public static string GenerateConcurrencyToken(uint version, string concurrencySecret)
    {
        string concurrencyToken;
        
        using (var sha = SHA256.Create())
        {
            var rawToken = version.ToString() + concurrencySecret;
            var rawTokenBytes = Encoding.UTF8.GetBytes(rawToken);
            var hashedTokenBytes = sha.ComputeHash(rawTokenBytes);
            var hashedToken = Convert.ToBase64String(hashedTokenBytes);
            
            concurrencyToken = hashedToken;
        }

        return concurrencyToken;
    }

    public static bool ValidateConcurrencyToken(uint version, string concurrencySecret, string concurrencyToken)
    {
        var actualToken = GenerateConcurrencyToken(version, concurrencySecret);
        return actualToken == concurrencyToken;
    }

    public static string GenerateConcurrencySecret()
    {
        string concurrencySecret;
        
        using (var rng = RandomNumberGenerator.Create())
        {
            var secretBytes = new byte[16];
            rng.GetBytes(secretBytes);
            var secretString = Convert.ToBase64String(secretBytes);

            concurrencySecret = secretString;
        }

        return concurrencySecret;
    }
}