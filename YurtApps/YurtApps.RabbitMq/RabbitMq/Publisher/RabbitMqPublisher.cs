using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using YurtApps.Messaging.Contracts.Interfaces;
using YurtApps.Messaging.RabbitMq.Connection;

namespace YurtApps.Messaging.RabbitMq.Publisher
{
    public class RabbitMqPublisher<T> : IMessagePublisher<T>
    {
        private readonly IChannel _channel;
        private readonly string _queueName;

        public RabbitMqPublisher(IConnectionProvider provider)
        {
            _queueName = typeof(T).Name.ToLowerInvariant();

            var connection = provider.GetConnectionAsync().GetAwaiter().GetResult();
            _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();
        }

        public async Task PublishAsync(T message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await _channel.BasicPublishAsync(
                exchange: "",
                routingKey: _queueName,
                body: body
            );
        }
    }
}