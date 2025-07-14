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
        private readonly IConnectionProvider _provider;

        public RabbitMqConsumer(IConnectionProvider provider)
        {
            _provider = provider;
        }

        public async Task StartAsync(Func<T, Task> handleMessage)
        {
            var connection = await _provider.GetConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            var queueName = typeof(T).Name.ToLowerInvariant();

            await channel.QueueDeclareAsync
                (
                queue: queueName, 
                durable: true, 
                exclusive: false, 
                autoDelete: false
                );

            var consumer = new AsyncEventingBasicConsumer(channel);

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

            await channel.BasicConsumeAsync
                (
                queue: queueName, 
                autoAck: true, 
                consumer: consumer
                );

            Console.WriteLine($"Queue is listening: {queueName}");
        }
    }
}