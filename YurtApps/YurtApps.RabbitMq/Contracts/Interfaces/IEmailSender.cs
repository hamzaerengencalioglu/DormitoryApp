using YurtApps.Messaging.Contracts.Dtos;

namespace YurtApps.Messaging.Contracts.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(MailDto dto);
    }
}