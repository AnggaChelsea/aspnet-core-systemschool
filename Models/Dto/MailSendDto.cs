using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Models.Dto
{
    public class MailSendDto
    {
        public string Server { get; set; }
        public int port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}