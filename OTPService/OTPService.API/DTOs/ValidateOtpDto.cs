namespace OTPService.API.DTOs;

public class ValidateOtpDto
{
    public string PrimaryOtp { get; set; }
    public string? SecondaryOtp { get; set; }
}