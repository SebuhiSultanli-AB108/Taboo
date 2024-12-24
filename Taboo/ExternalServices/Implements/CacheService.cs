using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Taboo.ExternalServices.Abstracts;

namespace Taboo.ExternalServices.Implements;

public class CacheService(IDistributedCache _redis) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key)
    {
        string? data = await _redis.GetStringAsync(key);
        if (data is null) return default(T);
        return JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetAsync<T>(string key, T data, int seconds)
    {
        string json = JsonSerializer.Serialize(data);
        DistributedCacheEntryOptions opt = new();
        opt.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(seconds);
        await _redis.SetStringAsync(key, json, opt);
    }
    public async Task RemoveAsync(string key)
    {
        await _redis.RemoveAsync(key);
    }
}
