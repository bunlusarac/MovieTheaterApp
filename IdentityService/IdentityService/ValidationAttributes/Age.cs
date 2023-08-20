using System.ComponentModel.DataAnnotations;

namespace IdentityService.ValidationAttributes;

public class Age: ValidationAttribute
{
    public int MaxAge { get; set; }
    public int MinAge { get; set; }

    public Age()
    {
        MaxAge = Int32.MaxValue;
        MinAge = 0;
    }

    public override bool IsValid(object value)
    {
        var dob = (DateTime)value;

        var birthYear = dob.Year;
        var currentYear = DateTime.Now.Year;

        var age = currentYear - birthYear;

        return age >= MinAge && age <= MaxAge;
    }
}