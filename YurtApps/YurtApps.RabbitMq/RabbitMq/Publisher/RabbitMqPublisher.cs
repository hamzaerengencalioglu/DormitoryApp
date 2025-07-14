using System.Text.Json;
using System.Text;
using YurtApps.Messaging.Contracts.Interfaces;
using YurtApps.Messaging.RabbitMq.Connection;
using RabbitMQ.Client;

namespace YurtApps.Messaging.RabbitMq.Publisher
{
    public class RabbitMqPublisher<T> : IMessagePublisher<T>
    {
        private readonly IConnectionProvider _provider;

        public RabbitMqPublisher(IConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task PublishAsync(T message)
        {
            var connection = await _provider.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            var queueName = typeof(T).Name.ToLowerInvariant();
            await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            await channel.BasicPublishAsync(exchange: "", routingKey: queueName, body: body);
        }
    }

}