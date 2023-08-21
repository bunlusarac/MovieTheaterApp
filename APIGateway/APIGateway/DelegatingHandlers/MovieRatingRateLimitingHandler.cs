using APIGateway.Persistence;

namespace APIGateway.DelegatingHandlers;

public class MovieRatingRateLimitingHandler: TokenBasedRateLimitingHandler
{
    public MovieRatingRateLimitingHandler(ILogger<TokenBasedRateLimitingHandler> logger, IRedisRepository redisRepository) : base(logger, redisRepository)
    {
    }

    public override int MaxRequests { get; set; } = 1;
    public override TimeSpan TimeWindow { get; set; } = TimeSpan.MaxValue;
}