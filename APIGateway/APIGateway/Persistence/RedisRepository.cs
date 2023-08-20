namespace APIGateway.Persistence;

using StackExchange.Redis;

public class RedisRepository: IRedisRepository
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _redisDatabase;
    
    public RedisRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _redisDatabase = connectionMultiplexer.GetDatabase();
    }

    public void Set(string key, string value)
    {
        var db = _connectionMultiplexer.GetDatabase();
        db.StringSet(key, value);
    }

    public string Get(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return db.StringGet(key);
    }

    public void SetWithTtl(string key, string value, TimeSpan ttl)
    {
        var db = _connectionMultiplexer.GetDatabase();
        db.StringSet(key, value, ttl);
    }

    public async Task<long> Increment(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return await db.StringIncrementAsync(key);
    }
    
    public async Task<bool> SetExpiration(string key, TimeSpan timeWindow)
    {
        var db = _connectionMultiplexer.GetDatabase();
        return await db.KeyExpireAsync(key, timeWindow);
    }
    
}