using System.Net;
using System.Net.Mail;

namespace YurtApps.RabbitMq
{
    public class MailSender
    {
        public static async Task Send(MailDto dto)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("ac29e3bbd872ef", "32d7a79ad82436"),
                EnableSsl = true
            };

            var message = new MailMessage
                (
                "no-reply@yurtapps.com",
                dto.To,
                dto.Subject,
                dto.Body
                );
            await client.SendMailAsync(message);
        }
    }
}
