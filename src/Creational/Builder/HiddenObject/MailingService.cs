using Microsoft.Extensions.Options;

namespace Creational.Builder.HiddenObject;

public class MailingService
{
    private readonly ExternalPackage.ISmtpClient _smtpClient;
    private readonly IOptions<SecretCredentialsOptions> _options;

    internal MailingService(
        ExternalPackage.ISmtpClient smtpClient,
        IOptions<SecretCredentialsOptions> options)
    {
        _smtpClient = smtpClient;
        _options = options;
    }

    public void SendMail(Action<IEmailBuilder> setup)
    {
        var email = new ExternalPackage.Email();
        
        setup(new EmailBuilder(email));
        
        EnrichSecretCredentials(email);

        _smtpClient.Send(email);
    }

    private void EnrichSecretCredentials(ExternalPackage.Email email)
    {
        var credentials = _options.Value;
        email.Certificate = credentials.Certificate;
        email.UserName = credentials.UserName;
        email.UserPassword = credentials.UserPassword;
    }

    private class EmailBuilder : IEmailBuilder
    {
        private readonly ExternalPackage.Email _email;

        public EmailBuilder(ExternalPackage.Email email)
        {
            _email = email;
        }

        public IEmailBuilder From(string from)
        {
            _email.From = from;
            return this;
        }

        public IEmailBuilder To(string to)
        {
            _email.To = to;
            return this;
        }

        public IEmailBuilder WithSubject(string subject)
        {
            _email.Subject = subject;
            return this;
        }

        public IEmailBuilder WithBody(string body)
        {
            _email.Body = body;
            return this;
        }
    }

    public class ExternalPackage
    {
        public interface ISmtpClient
        {
            bool Send(Email email);
        }

        public class Email
        {
            public string From { get; set; }
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public string Certificate { get; set; }
            public string UserName { get; set; }
            public string UserPassword { get; set; }
        }
    }
}

public class SecretCredentialsOptions
{
    public string Certificate { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
}