 namespace NetAngularAuthWebApi.Services;

 public class LocalMailService : IMailService {
    private string _mailTo = String.Empty;
    private string _mailFrom = String.Empty;

    private readonly IConfiguration _configuration;

    public LocalMailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Send(string subject, string message){
      Console.WriteLine($"{subject} {message}");
      Console.WriteLine($"{subject} Hallo");
      Console.WriteLine($"Message: {message}");
    }

   
}

