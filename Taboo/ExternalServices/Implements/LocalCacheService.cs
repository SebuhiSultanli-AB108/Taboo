using Microsoft.Extensions.Caching.Memory;
using Taboo.ExternalServices.Abstracts;

namespace Taboo.ExternalServices.Implements;

public class LocalCacheService(IMemoryCache _cache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key)
    {
        T? value = default(T);
        await Task.Run(() =>
        {
            _cache.TryGetValue<T>(key, out value);
        });
        return value;
    }

    public async Task SetAsync<T>(string key, T data, int seconds)
    {
        await Task.Run(() =>
        {
            _cache.Set<T>(key, data, DateTime.Now.AddSeconds(seconds));
        });
    }
    public async Task RemoveAsync(string key)
    {
        _cache.Remove(key);
    }
}
