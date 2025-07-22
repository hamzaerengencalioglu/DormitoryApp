using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion;
using YurtApps.Caching.Configurations;
using Microsoft.Extensions.Caching.Distributed;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace YurtApps.Caching.Extensions
{
    public static class CacheServiceExtensions
    {
        public static IServiceCollection AddRedisAndFusionCache(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = new RedisSettings();
            configuration.GetSection("RedisSettings").Bind(redisSettings);

            var multiplexer = ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisSettings.ConnectionString;
                options.InstanceName = redisSettings.InstanceName;
            });

            services.AddFusionCache() 
                .WithDefaultEntryOptions(new FusionCacheEntryOptions
                {
                    Duration = TimeSpan.FromHours(24),
                    IsFailSafeEnabled = true,
                    FailSafeThrottleDuration = TimeSpan.FromSeconds(30),
                })
                .WithSerializer(new FusionCacheSystemTextJsonSerializer())
                .WithDistributedCache(sp =>
                {
                    return sp.GetRequiredService<IDistributedCache>();
                });

            return services;
        }
    }
}