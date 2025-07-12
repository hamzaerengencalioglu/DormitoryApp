namespace YurtApps.Application.Interfaces
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body);
    }
}
