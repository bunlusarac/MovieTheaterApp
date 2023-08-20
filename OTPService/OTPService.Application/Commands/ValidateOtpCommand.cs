using MediatR;
using Microsoft.Extensions.Logging;
using OtpNet;
using OTPService.Application.Common;
using OTPService.Application.Communicators;
using OTPService.Application.DTOs;
using OTPService.Application.Messages;
using OTPService.Application.Persistence;

namespace OTPService.Application.Commands;

public class ValidateOtpCommand: IRequest<ShortSessionCreatedDto>
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

public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, ShortSessionCreatedDto>
    {
        private readonly IOtpUserRepository _repository;
        private readonly ILogger<ValidateOtpCommandHandler> _logger;
        //private readonly IRabbitCommunicator _rabbitCommunicator;
        private readonly IIdentityServiceCommunicator _identityServiceCommunicator;
        
        /*
        public ValidateOtpCommandHandler(IRabbitCommunicator rabbitCommunicator, IOtpUserRepository repository, ILogger<ValidateOtpCommandHandler> logger)
        {
            _rabbitCommunicator = rabbitCommunicator;
            _repository = repository;
            _logger = logger;
        }*/

        public ValidateOtpCommandHandler(IOtpUserRepository repository, ILogger<ValidateOtpCommandHandler> logger, IIdentityServiceCommunicator identityServiceCommunicator)
        {
            _repository = repository;
            _logger = logger;
            _identityServiceCommunicator = identityServiceCommunicator;
        }

        public async Task<ShortSessionCreatedDto> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
        {
         
            var user = await _repository.GetByIssuedUserId(request.UserId);
            /*
            if (user == null) return Result.Error;
            if (user.IsDisposed) return Result.Error;
            */
            
            if (user == null) throw new InvalidOperationException(); //TODO
            if (user.IsDisposed) throw new InvalidOperationException(); //TODO

            
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
                    //TODO Strategy
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
                
                
                /*
                var message = new OtpValidatedMessage
                {
                    UserId = user.IssuedUserId
                };
                */      
                //_rabbitCommunicator.SendMessageToExchange("mta_exchange", message);

                var saveResult = await _repository.Update(user) == Result.Error ? Result.Error : validationResult;
                if (saveResult == Result.Error) throw new InvalidOperationException();
                
                var dto = await _identityServiceCommunicator.RequestShortSessionCreation(user.IssuedUserId);
                return dto;
            }
            catch (Exception e)
            {
                throw; //TODO
            }   
        }
    }