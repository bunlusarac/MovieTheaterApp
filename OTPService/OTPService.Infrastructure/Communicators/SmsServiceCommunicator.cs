using Microsoft.Extensions.Logging;
using OTPService.Application.Common;
using OTPService.Application.Communicators;

namespace OTPService.Infrastructure.Communicators;

public class SmsServiceCommunicator: ISmsServiceCommunicator
{
    private readonly ILogger<SmsServiceCommunicator> _logger;

    public SmsServiceCommunicator(ILogger<SmsServiceCommunicator> logger)
    {
        _logger = logger;
    }
    
    public Task<Result> SendOtpSms(string phoneNumber, string otp)
    {
        _logger.Log(LogLevel.Information, $"Sent the OTP {otp} to gsm {phoneNumber}.");
        return Task.FromResult(Result.Ok);
    }
}