using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetAngularAuthWebApi.Services.Email.Model;

namespace NetAngularAuthWebApi.Services.Email
{
    public interface IEmailService
    {
        void SendEmail(EmailDto emailDto);
    }
}