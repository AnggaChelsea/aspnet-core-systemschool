using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.helpers
{
    public class IEmailSender
    {
         public Task SendEmail(string email, string subject, string message){
                var mail = "kunpayakun091@gmail.com";
                var pw = "Sayangmamah1.";

                var client = new SmtpClient("smtp-mail.outlook.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail, pw)
                };

                return client.SendMailAsync(new MailMessage(
                    from: mail,
                    to: email,
                    subject,
                    message
                ));
        }

        public static void MailSend(string from, string to, string subject, string message){
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("a989fe06692cba", "********18fe"),
                EnableSsl = true
            };
            client.Send("from@example.com", "to@example.com", "Hello world", "testbody");
            Console.WriteLine("Sent");
            Console.ReadLine();
        }
    }

}