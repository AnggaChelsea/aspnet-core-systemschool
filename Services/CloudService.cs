using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetAngularAuthWebApi.Services
{
    public class CloudService
    {
         private string _mailTo = "admin@gmail.com";
    private string _mailFrom = "noreply@mycompany.com";

    public void Send(string subject, string message){
      Console.WriteLine($"{subject} {message}" + $"with {nameof(CloudService)}");
      Console.WriteLine($"{subject} Hallo");
      Console.WriteLine($"Message: {message}");
    }
    }
}