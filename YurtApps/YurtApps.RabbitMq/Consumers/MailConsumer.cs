using MassTransit;
using System.Text.Json;
using YurtApps.Messaging.Contracts.Dtos;
using YurtApps.Messaging.Contracts.Interfaces;

namespace YurtApps.Messaging.Consumers
{
    public class MailConsumer : IConsumer<MailDto>
    {
        private readonly IMailSender _mailSender;

        public MailConsumer(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public async Task Consume(ConsumeContext<MailDto> context)
        {
            var dto = context.Message;

            Console.WriteLine($"Sending Mail:");
            Console.WriteLine(JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true }));

            await _mailSender.SendAsync(dto);

            Console.WriteLine($"Sent {dto.To}");
        }
    }
}