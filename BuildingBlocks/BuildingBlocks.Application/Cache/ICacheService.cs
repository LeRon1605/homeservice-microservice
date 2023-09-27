namespace BuildingBlocks.Application.Cache;

public interface ICacheService
{
    Task SetCachedDataAsync<T>(string key, T data, TimeSpan cacheDuration);

    Task<T?> GetCachedDataAsync<T>(string key);

    Task RemoveCachedDataAsync(string key);
}