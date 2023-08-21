using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtpNet;
using OTPService.Application.Common;
using OTPService.Application.Communicators;
using OTPService.Application.Persistence;
using OTPService.Application.Utils;

namespace OTPService.Application.Commands;

public class IssueOtpCommand: IRequest<Result>
{
    public Guid UserId;
    //public OtpClaim PrimaryOtpClaim;
    //public string? EmailAddress;
    //public string? PhoneNumber;
    public string Bearer;

    public IssueOtpCommand(string bearer)
    {
        Bearer = bearer;
    }
}

public class IssueOtpCommandHandler: IRequestHandler<IssueOtpCommand, Result>
    {
        private readonly IOtpUserRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IEmailServiceCommunicator _emailServiceCommunicator;
        private readonly ISmsServiceCommunicator _smsServiceCommunicator;
        private readonly IIdentityServiceCommunicator _identityServiceCommunicator;
        private readonly ILogger<IssueOtpCommandHandler> _logger;

        public IssueOtpCommandHandler(
            IOtpUserRepository repository, 
            IConfiguration configuration, 
            IEmailServiceCommunicator emailServiceCommunicator, 
            ISmsServiceCommunicator smsServiceCommunicator, 
            IIdentityServiceCommunicator identityServiceCommunicator,
            ILogger<IssueOtpCommandHandler> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _emailServiceCommunicator = emailServiceCommunicator;
            _smsServiceCommunicator = smsServiceCommunicator;
            _identityServiceCommunicator = identityServiceCommunicator;
            _logger = logger;
        }

        public async Task<Result> Handle(IssueOtpCommand request, CancellationToken cancellationToken)
        { 
            try
            {
                var userInfo = await _identityServiceCommunicator.SendGetUserInfoRequest(request.Bearer);
                
                var otpUser = await _repository.GetByIssuedUserId(userInfo.SubjectId);
                if (otpUser == null) return Result.Error;
                
                otpUser.RequestNewOtp();
                
                if (await _repository.Update(otpUser) == Result.Error)
                {
                    return Result.Error;
                }

                var otpSize = int.Parse(_configuration.GetSection("OtpSize").Value);

                var primaryOtp = new Hotp(otpUser.PrimarySecret, hotpSize: otpSize).ComputeHOTP(otpUser.PrimaryCounter);
                var secondaryOtp = new Hotp(otpUser.SecondarySecret, hotpSize: otpSize).ComputeHOTP(otpUser.SecondaryCounter);
                
                //_logger.Log(LogLevel.Information, $"Primary: {primaryOtp} Secondary: {secondaryOtp}");
                
                await _emailServiceCommunicator.SendEmailOtp(userInfo.Email, primaryOtp);
                if (otpUser.MfaEnabled) await _smsServiceCommunicator.SendOtpSms(userInfo.PhoneNumber, secondaryOtp);

                /*
                if (otpUser.MfaEnabled)
                {
                    if(string.IsNullOrWhiteSpace(request.PhoneNumber) && string.IsNullOrWhiteSpace(request.EmailAddress))
                        return Result.Error;

                    switch (request.PrimaryOtpClaim)
                    {
                        case OtpClaim.PhoneNumber:
                            await _smsServiceCommunicator.SendOtpSms(request.PhoneNumber, primaryOtp);
                            await _emailServiceCommunicator.SendEmailOtp(request.EmailAddress, secondaryOtp);
                            
                            break;
                        case OtpClaim.Email :
                            await _emailServiceCommunicator.SendEmailOtp(request.EmailAddress, primaryOtp);
                            await _smsServiceCommunicator.SendOtpSms(request.PhoneNumber, secondaryOtp);
                            
                            break;
                        default:
                            return Result.Error;
                    }
                    
                    return Result.Ok;
                }

                switch (request.PrimaryOtpClaim)
                {
                    case OtpClaim.Email:
                        if(string.IsNullOrEmpty(request.EmailAddress))
                            return Result.Error;

                        await _emailServiceCommunicator.SendEmailOtp(request.EmailAddress, primaryOtp);
                        break;
                    case OtpClaim.PhoneNumber:
                        if(string.IsNullOrEmpty(request.PhoneNumber))
                            return Result.Error;
                            
                        await _smsServiceCommunicator.SendOtpSms(request.PhoneNumber, primaryOtp);
                        break;
                    default:
                        return Result.Error;
                }
                */
                           
                return Result.Ok;
            }
            catch (Exception e)
            {
                return Result.Error;
            }
        }
    }