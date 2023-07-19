using System.Security.Cryptography;

namespace OTPService.Application.Utils;

public static class SecretGenerator
{
    public static byte[] Generate(int secretSize)
    {
        var secret = new byte[secretSize];
        
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(secret);

        return secret;
    }
}