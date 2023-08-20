using IdentityService.DTOs;
using IdentityService.Persistence;
using MediatR;

namespace IdentityService.Queries;

public class ValidateShortSessionQuery: IRequest<ShortSessionValidityDto>
{
    public string ShortSessionToken { get; set; }
    public Guid UserId { get; set; }

    public ValidateShortSessionQuery(string shortSessionToken, Guid userId)
    {
        ShortSessionToken = shortSessionToken;
        UserId = userId;
    }
}

public class ValidateShortSessionQueryHandler : IRequestHandler<ValidateShortSessionQuery, ShortSessionValidityDto>
{
    private readonly IRedisRepository _redisRepository;

    public ValidateShortSessionQueryHandler(IRedisRepository redisRepository)
    {
        _redisRepository = redisRepository;
    }
    
    public async Task<ShortSessionValidityDto> Handle(ValidateShortSessionQuery request, CancellationToken cancellationToken)
    {
        var userId = _redisRepository.Get(request.ShortSessionToken);

        if (userId is null) 
            return new ShortSessionValidityDto() { IsValid = false }; //TODO

        if (Guid.Parse(userId) != request.UserId)
            return new ShortSessionValidityDto() { IsValid = false }; //TODO
        
        return new ShortSessionValidityDto() { IsValid = true };
    }
}