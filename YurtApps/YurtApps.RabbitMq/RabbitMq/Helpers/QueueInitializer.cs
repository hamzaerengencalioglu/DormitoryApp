using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YurtApps.Messaging.RabbitMq.Connection;

namespace YurtApps.Messaging.RabbitMq.Helpers
{
    public static class QueueInitializer
    {
        public static async Task DeclareQueueAsync<T>(IConnectionProvider provider)
        {
            var connection = await provider.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            var queueName = typeof(T).Name.ToLowerInvariant();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );
        }
    }
}