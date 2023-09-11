namespace BuildingBlocks.Application.Cache;

public interface ICacheService
{
    Task<T> GetOrAddAsync<T>(string id, Func<Task<T>> factory, TimeSpan expireTime);

    Task<bool> SetRecordAsync<T>(string id, T data, TimeSpan expireTime);

    Task<T> GetRecordAsync<T>(string id);

    Task<bool> RemoveRecordAsync(string id);

    Task ClearAsync();
}