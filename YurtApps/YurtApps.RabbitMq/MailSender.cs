using System.Net;
using System.Net.Mail;

namespace YurtApps.RabbitMq
{
    public interface IMailSender
    {
        Task Send(MailDto dto);
    }

    public class MailSender : IMailSender
    {
        public async Task Send(MailDto dto)
        {
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("ac29e3bbd872ef", "32d7a79ad82436"),
                EnableSsl = true
            };

            var message = new MailMessage(
                from: "yurtapps@gmail.com",
                to: dto.To,
                subject: dto.Subject,
                body: dto.Body
            );

            await client.SendMailAsync(message);
        }
    }
}