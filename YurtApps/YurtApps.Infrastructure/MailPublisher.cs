using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using YurtApps.Application.Dtos;
using YurtApps.Application.Interfaces;

namespace YurtApps.Infrastructure
{
    public class MailPublisher : IMailPublisher
    {
        public async Task Publish(MailDto dto)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync
                (
                queue: "mail",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            var mailBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dto));

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "mail",
                mandatory: true,
                basicProperties: new BasicProperties { Persistent = true},
                body : mailBody
            );

            await channel.CloseAsync();
            await connection.CloseAsync();
        }
    }
}
