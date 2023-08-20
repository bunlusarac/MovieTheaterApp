using System.Net;
using APIGateway.Persistence;

namespace APIGateway.DelegatingHandlers;

public class TokenBasedRateLimitingHandler: DelegatingHandler
{
    private readonly ILogger<TokenBasedRateLimitingHandler> _logger;
    private readonly IRedisRepository _redisRepository;

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
        const int maxRequests = 3;
        var timeWindow = TimeSpan.FromHours(3);
        
        var key = $"{userId}:{endpoint}";
        var requestCount = await _redisRepository.Increment(key);

        if (requestCount > maxRequests)
        {
            return new HttpResponseMessage(HttpStatusCode.TooManyRequests);
        }

        await _redisRepository.SetExpiration(key, timeWindow);
        return await base.SendAsync(request, cancellationToken);
    }
}