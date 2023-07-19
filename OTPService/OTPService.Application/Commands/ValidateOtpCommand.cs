using MediatR;
using Microsoft.Extensions.Logging;
using OtpNet;
using OTPService.Application.Common;
using OTPService.Application.Persistence;

namespace OTPService.Application.Commands;

public class ValidateOtpCommand: IRequest<Result>
{
    public string PrimaryOtp;
    public string SecondaryOtp;
    public Guid UserId;

    public ValidateOtpCommand(string primaryOtp, string secondaryOtp, Guid userId)
    {
        PrimaryOtp = primaryOtp;
        SecondaryOtp = secondaryOtp;
        UserId = userId;
    }
}

public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, Result>
    {
        private readonly IOtpUserRepository _repository;
        private readonly ILogger<ValidateOtpCommandHandler> _logger;

        public ValidateOtpCommandHandler(IOtpUserRepository repository, ILogger<ValidateOtpCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
        {
         
            var user = await _repository.GetByIssuedUserId(request.UserId);
            if (user == null) return Result.Error;
            if (user.IsDisposed) return Result.Error;
            
            var truthPrimaryOtp = new Hotp(user.PrimarySecret);
            var truthSecondaryOtp = new Hotp(user.SecondarySecret);

            var primaryValidity = truthPrimaryOtp.VerifyHotp(request.PrimaryOtp, user.PrimaryCounter);
            var secondaryValidity = truthSecondaryOtp.VerifyHotp(request.SecondaryOtp, user.SecondaryCounter);

            try
            {
                Result validationResult;

                if (user.MfaEnabled)
                {
                    if (primaryValidity && secondaryValidity)
                    {
                        user.ValidateOtp();
                        validationResult = Result.Ok;
                    }
                    else
                    {
                        user.IncrementFailedAttempts();
                        validationResult = Result.Error;
                    }
                }
                else
                {

                    if (primaryValidity)
                    {
                        

                        user.ValidateOtp();
                        validationResult = Result.Ok;
                    }
                    else
                    {
                        user.IncrementFailedAttempts();
                        validationResult = Result.Error;
                    }
                }

                return await _repository.Update(user) == Result.Error ? Result.Error : validationResult;
            }
            catch (Exception e)
            {
                return Result.Error;
            }   
        }
    }