using StackExchange.Redis;

namespace APIGateway.Persistence;

public interface IRedisRepository
{
    public void SetWithTtl(string key, string value, TimeSpan ttl);
    public void Set(string key, string value);
    public string Get(string key);
    public Task<long> Increment(string key);
    public Task<bool> SetExpiration(string key, TimeSpan timeWindow);
}