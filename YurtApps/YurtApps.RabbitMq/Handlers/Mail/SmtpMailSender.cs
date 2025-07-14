using System.Net.Mail;
using System.Net;
using YurtApps.Messaging.Contracts.Interfaces;
using YurtApps.Messaging.Contracts.Dtos;

namespace YurtApps.Messaging.Handlers.Mail
{
    public class SmtpMailSender : IEmailSender
    {
        public async Task SendAsync(MailDto dto)
        {
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("ac29e3bbd872ef", "32d7a79ad82436"),
                EnableSsl = true
            };

            var message = new MailMessage("from@example.com", dto.To, dto.Subject, dto.Body);
            await client.SendMailAsync(message);
        }
    }
}