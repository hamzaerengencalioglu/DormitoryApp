using YurtApps.Messaging.Contracts.Dtos;
using YurtApps.Messaging.Contracts.Interfaces;

namespace YurtApps.Messaging.Handlers.Mail
{
    public class MailMessageHandler : IMessageHandler<MailDto>
    {
        private readonly IEmailSender _emailSender;

        public MailMessageHandler(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task HandleAsync(MailDto message)
        {
            await _emailSender.SendAsync(message);
        }
    }
}