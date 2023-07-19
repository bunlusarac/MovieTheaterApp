using OTPService.Application.Utils;

namespace OTPService.API.DTOs;

public class RequestOtpDto
{
    public Guid UserId { get; set; }
    public string? EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public OtpClaim PrimaryOtpClaim { get; set; }
}