using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IdentityService.ValidationAttributes;

public class AlphabeticalAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var input = value as string;
        return Regex.IsMatch(input ?? throw new InvalidOperationException(), "^[a-zA-Z]+$");
    }
}