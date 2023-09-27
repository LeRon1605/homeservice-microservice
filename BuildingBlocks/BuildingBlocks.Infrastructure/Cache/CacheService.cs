using System.Text.Json;
using BuildingBlocks.Application.Cache;
using Microsoft.Extensions.Caching.Distributed;

namespace BuildingBlocks.Infrastructure.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task SetCachedDataAsync<T>(string key, T data, TimeSpan cacheDuration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = cacheDuration
        };
        var jsonData = JsonSerializer.Serialize(data);
        await _cache.SetStringAsync(key, jsonData, options);
    }

    public async Task<T?> GetCachedDataAsync<T>(string key)
    {
        var jsonData = await _cache.GetStringAsync(key);
        if (jsonData == null)
            return default;
        
        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public async Task RemoveCachedDataAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}