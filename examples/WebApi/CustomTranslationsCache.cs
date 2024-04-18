using System.Collections.Concurrent;
using translord;

namespace WebApi;

public class CustomTranslationsCache(ConcurrentDictionary<string, string> cache) : ITranslationsCache
{
    public Task Add(string key, string value)
    {
        cache.TryAdd(key, value);
        return Task.CompletedTask;
    }

    public Task<string?> Get(string key)
    {
        cache.TryGetValue(key, out var value);
        return Task.FromResult(value);
    }

    public Task Remove(string key)
    {
        cache.TryRemove(key, out _);
        return Task.CompletedTask;
    }
}