namespace IdentityService.Persistence;

public interface IRedisRepository
{
    public void SetWithTtl(string key, string value, TimeSpan ttl);
    public void Set(string key, string value);
    public string Get(string key);
}