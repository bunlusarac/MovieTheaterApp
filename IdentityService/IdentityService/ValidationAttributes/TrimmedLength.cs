using System.ComponentModel.DataAnnotations;

namespace IdentityService.ValidationAttributes;

public class TrimmedLength: ValidationAttribute
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }

    public TrimmedLength()
    {
        Minimum = 0;
        Maximum = int.MaxValue;
    }

    public override bool IsValid(object value)
    {
        var input = value as string;

        if (input is null) throw new InvalidOperationException();

        var trimmedInput = input.Trim();
        var trimmedLength = trimmedInput.Length;
        
        return trimmedLength >= Minimum && trimmedLength <= Maximum;
    }
}