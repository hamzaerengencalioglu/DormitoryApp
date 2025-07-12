using Microsoft.AspNetCore.Mvc;
using YurtApps.Application.Interfaces;

namespace YurtApps.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("send-mail")]
        public async Task<IActionResult> SendMail()
        {
            await _mailService.SendMailAsync("test@example.com", "Test Başlık", "Bu bir test mailidir.");
            return Ok("Mail gönderildi.");
        }
    }
}
