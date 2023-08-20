using System.Security.Cryptography;
using IdentityService.DTOs;
using IdentityService.Persistence;
using MediatR;

namespace IdentityService.Commands;

public class CreateShortSessionCommand: IRequest<ShortSessionCreatedDto>
{
    public Guid UserId { get; set; }

    public CreateShortSessionCommand(Guid userId)
    {
        UserId = userId;
    }
}

public class CreateShortSessionCommandHandler: IRequestHandler<CreateShortSessionCommand, ShortSessionCreatedDto>
{
    private readonly IRedisRepository _redisRepository;

    public CreateShortSessionCommandHandler(IRedisRepository redisRepository)
    {
        _redisRepository = redisRepository;
    }

    public async Task<ShortSessionCreatedDto> Handle(CreateShortSessionCommand request, CancellationToken cancellationToken)
    {
        var tokenBytes = new byte[16];
        string token;
        
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(tokenBytes);
            token = Convert.ToBase64String(tokenBytes);
        }
        
        _redisRepository.SetWithTtl(token, request.UserId.ToString(), TimeSpan.FromSeconds(90));

        return new ShortSessionCreatedDto
        {
            ShortSessionToken = token
        };
    }
}