namespace OTPService.API.DTOs;

public class ValidateOtpDto
{
    public Guid UserId { get; set; }
    public string PrimaryOtp { get; set; }
    public string? SecondaryOtp { get; set; }
}