using MediatR;
using Microsoft.Extensions.Logging;
using OTPService.Application.Common;
using OTPService.Application.Persistence;

namespace OTPService.Application.Commands;

public class UpdateOtpUserMfaStatusCommand: IRequest<Result>
{
    public Guid UserId;
    public bool MfaEnabled;

    public UpdateOtpUserMfaStatusCommand(Guid userId, bool mfaEnabled)
    {
        UserId = userId;
        MfaEnabled = mfaEnabled;
    }
}


public class UpdateOtpUserMfaStatusCommandHandler : IRequestHandler<UpdateOtpUserMfaStatusCommand, Result>
{
    private readonly IOtpUserRepository _otpUserRepository;
    private readonly ILogger<UpdateOtpUserMfaStatusCommandHandler> _logger;

    public UpdateOtpUserMfaStatusCommandHandler(IOtpUserRepository repository, ILogger<UpdateOtpUserMfaStatusCommandHandler> logger)
    {
        _otpUserRepository = repository;
        _logger = logger;
    }
        
    public async Task<Result> Handle(UpdateOtpUserMfaStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _otpUserRepository.GetByIssuedUserId(request.UserId);
        if (user == null) return Result.Error;
            
        user.SwitchMfaStatus(request.MfaEnabled);
        return await _otpUserRepository.Update(user);
    }
}