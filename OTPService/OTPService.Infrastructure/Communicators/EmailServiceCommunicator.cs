using Microsoft.Extensions.Logging;
using OTPService.Application.Common;
using OTPService.Application.Communicators;

namespace OTPService.Infrastructure.Communicators;

public class EmailServiceCommunicator: IEmailServiceCommunicator
{
    private readonly ILogger<EmailServiceCommunicator> _logger;

    public EmailServiceCommunicator(ILogger<EmailServiceCommunicator> logger)
    {
        _logger = logger;
    }

    public Task<Result> SendEmailOtp(string emailAddress, string otp)
    {
        _logger.Log(LogLevel.Information, $"Sent the OTP {otp} to email {emailAddress}.");
        return Task.FromResult(Result.Ok);
    }
}