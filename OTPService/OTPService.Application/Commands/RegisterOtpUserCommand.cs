using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OtpNet;
using OTPService.Application.Common;
using OTPService.Application.Persistence;
using OTPService.Domain.Entities;

namespace OTPService.Application.Commands;

public class RegisterOtpUserCommand: IRequest<Result>
{
    public Guid UserId;

    public RegisterOtpUserCommand(Guid userId)
    {
        UserId = userId;
    }
}

public class RegisterOtpUserCommandHandler : IRequestHandler<RegisterOtpUserCommand, Result>
{
    private readonly IOtpUserRepository _repository;
    private IConfiguration _configuration;
    private readonly ILogger<RegisterOtpUserCommandHandler> _logger;

    public RegisterOtpUserCommandHandler(IOtpUserRepository repository, IConfiguration configuration, ILogger<RegisterOtpUserCommandHandler> logger)
    {
        _repository = repository;
        _configuration = configuration;
        _logger = logger;
    }
        
    public async Task<Result> Handle(RegisterOtpUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _repository.GetByIssuedUserId(request.UserId);
        if (existingUser != null) return Result.Error;

        //TODO
        var secretSize = int.Parse(_configuration.GetSection("SecretSize").Value);
        var blockTimeout = int.Parse(_configuration.GetSection("BlockTimeout").Value);
        var maxDisposals = int.Parse(_configuration.GetSection("MaxDisposals").Value);
        var maxRetries = int.Parse(_configuration.GetSection("MaxRetries").Value);
        var otpTimeWindow = int.Parse(_configuration.GetSection("OtpTimeWindow").Value);

        var newUser = new OtpUser(request.UserId,
            KeyGeneration.GenerateRandomKey(secretSize),
            KeyGeneration.GenerateRandomKey(secretSize),
            maxRetries,
            maxDisposals,
            TimeSpan.FromSeconds(otpTimeWindow),
            TimeSpan.FromSeconds(blockTimeout)
        );

        return await _repository.Add(newUser);
    }
}