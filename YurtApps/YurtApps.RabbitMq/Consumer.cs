using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace YurtApps.RabbitMq
{
    public class Consumer
    {
        public async Task Start()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

             await channel.QueueDeclareAsync(
                queue: "mail-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    var dto = JsonSerializer.Deserialize<MailDto>(json);
                    if (dto != null)
                    {
                        Console.WriteLine($"Sending Mail: {dto.To}");
                        await MailSender.Send(dto);
                        Console.WriteLine("Sent.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            };

            await channel.BasicConsumeAsync
                (
                queue: "mail-queue",
                autoAck: true,
                consumer: consumer
                );
        }
    }
}
