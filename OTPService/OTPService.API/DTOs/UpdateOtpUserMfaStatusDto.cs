namespace OTPService.API.DTOs;

public class UpdateOtpUserMfaStatusDto
{
    public Guid UserId { get; set; }
    public bool MfaEnabled { get; set; }
}