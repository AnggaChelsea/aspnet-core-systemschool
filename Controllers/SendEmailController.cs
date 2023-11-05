using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace NetAngularAuthWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SendEmailController : ControllerBase
    {
        [HttpPost("send-email-test")]
        public IActionResult SendEmail(string body){
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("taylor95@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("taylor95@ethereal.email"));
            email.Subject = "Email Address tes";
            email.Body = new TextPart(TextFormat.Html){
                Text = body
            };

            using var smtp = new SmtpClient();
            // smtp.Connect("smtp.gmail.email");
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("taylor95@ethereal.email", "nBzzX9q5gDq8yWv7er");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }

    
}