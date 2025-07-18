namespace YurtApps.Caching.Configurations
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; } = "YurtAppsCacheInstance";
    }
}