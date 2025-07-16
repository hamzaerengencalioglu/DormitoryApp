using YurtApps.Messaging.Contracts.Dtos;

namespace YurtApps.Messaging.Contracts.Interfaces
{
    public interface IMailSender
    {
        Task SendAsync(MailDto dto);
    }
}