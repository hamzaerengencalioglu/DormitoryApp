using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using YurtApps.Caching.Configurations;
using YurtApps.Caching.Interfaces;
using YurtApps.Caching.Services;

namespace YurtApps.Caching.Extensions
{
    public static class RedisServiceExtensions
    {
        public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            var redisSettings = new RedisSettings();
            configuration.GetSection("RedisSettings").Bind(redisSettings);

            var multiplexer = ConnectionMultiplexer.Connect(redisSettings.ConnectionString);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            services.AddSingleton<IRedisService, RedisService>();

            return services;
        }
    }
}