using System.Net;
using APIGateway.Persistence;

namespace APIGateway.DelegatingHandlers;

public class TokenBasedRateLimitingHandler: DelegatingHandler
{
    private readonly ILogger<TokenBasedRateLimitingHandler> _logger;
    private readonly IRedisRepository _redisRepository;

    public virtual int MaxRequests { get; set; } = 3;
    public virtual TimeSpan TimeWindow { get; set; } = TimeSpan.FromHours(3);
    
    public TokenBasedRateLimitingHandler(ILogger<TokenBasedRateLimitingHandler> logger, IRedisRepository redisRepository)
    {
        _logger = logger;
        _redisRepository = redisRepository;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var userId = request.Headers.GetValues("Customer-Id").FirstOrDefault();
        var endpoint = request.RequestUri.AbsolutePath;

        if (userId is null || string.IsNullOrWhiteSpace(endpoint))
            return new HttpResponseMessage(HttpStatusCode.Unauthorized);

        //TODO get from configuration
        
        
        var key = $"{userId}:{endpoint}";
        var requestCount = await _redisRepository.Increment(key);

        if (requestCount > MaxRequests)
        {
            return new HttpResponseMessage(HttpStatusCode.TooManyRequests);
        }

        await _redisRepository.SetExpiration(key, TimeWindow);
        return await base.SendAsync(request, cancellationToken);
    }
}