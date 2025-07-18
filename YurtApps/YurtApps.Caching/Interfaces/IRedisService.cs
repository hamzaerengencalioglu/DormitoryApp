namespace YurtApps.Caching.Interfaces
{
    public interface IRedisService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, string value, TimeSpan? expiry = null);
        Task<bool> RemoveAsync(string key);
    }
}