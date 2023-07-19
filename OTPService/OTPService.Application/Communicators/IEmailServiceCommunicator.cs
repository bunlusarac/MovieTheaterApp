using OTPService.Application.Common;

namespace OTPService.Application.Communicators;

public interface IEmailServiceCommunicator
{
    Task<Result> SendEmailOtp(string emailAddress, string otp);
}