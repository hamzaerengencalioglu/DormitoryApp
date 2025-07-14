using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using YurtApps.Messaging.Contracts.Interfaces;
using YurtApps.Messaging.RabbitMq.Connection;

namespace YurtApps.Messaging.RabbitMq.Consumer
{
    public class RabbitMqConsumer<T> : IMessageConsumer<T>
    {
        private readonly string _queueName;
        private readonly IChannel _channel;

        public RabbitMqConsumer(IConnectionProvider provider)
        {
            _queueName = typeof(T).Name.ToLowerInvariant();

            var connection = provider.GetConnectionAsync().GetAwaiter().GetResult();
            _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();
        }

        public async Task StartAsync(Func<T, Task> handleMessage)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var message = JsonSerializer.Deserialize<T>(json);

                    if (message != null)
                    {
                        Console.WriteLine($"Message sent: {typeof(T).Name} → {json}");

                        await handleMessage(message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] {ex.Message}");
                }
            };

            await _channel.BasicConsumeAsync(
                queue: _queueName,
                autoAck: true,
                consumer: consumer
            );
            Console.WriteLine($"Queue is listening: {_queueName}");
        }
    }
}