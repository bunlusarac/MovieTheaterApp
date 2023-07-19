using Microsoft.Extensions.Configuration;
using OTPService.Application.Common;

namespace OTPService.Application.Communicators;

public interface ISmsServiceCommunicator
{
    Task<Result> SendOtpSms(string phoneNumber, string otp);
}