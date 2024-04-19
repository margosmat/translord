using StackExchange.Redis;

namespace translord.RedisCache;

internal class TranslordRedisCache(IConnectionMultiplexer redis) : ITranslationsCache
{
    public async Task Add(string key, string value)
    {
        var db = redis.GetDatabase();
        await db.StringSetAsync(key, value);
    }

    public async Task<string?> Get(string key)
    {
        var db = redis.GetDatabase();
        return await db.StringGetAsync(key);
    }

    public async Task Remove(string key)
    {
        var db = redis.GetDatabase();
        await db.KeyDeleteAsync(key);
    }
}