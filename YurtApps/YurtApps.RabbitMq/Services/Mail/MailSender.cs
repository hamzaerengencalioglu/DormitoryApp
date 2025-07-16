using System.Net;
using System.Net.Mail;
using YurtApps.Messaging.Contracts.Dtos;
using YurtApps.Messaging.Contracts.Interfaces;

namespace YurtApps.Messaging.Services.Mail
{
    public class MailSender : IMailSender
    {
        public async Task SendAsync(MailDto dto)
        {
            using var client = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("ac29e3bbd872ef", "32d7a79ad82436"),
                EnableSsl = true
            };

            var message = new MailMessage("yurtapps@gmail.com", dto.To)
            {
                Subject = dto.Subject,
                Body = dto.Body
            };

            await client.SendMailAsync(message);
        }
    }
}