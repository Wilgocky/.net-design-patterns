using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Creational.Builder.HiddenObject;

public class Examples
{
    private const string SomeCoolGuyEmail = "somecoolguy@gr8mail.com";
    private const string SomeCoolGirlEmail = "somecoolgirl@notsogr8mail.com";
    private const string SomeVeryInterestingTopic = "Some very interesting topic";
    private const string SomeBody = "body";
    private static readonly SecretCredentialsOptions Credentials = new()
    {
        Certificate = "cert",
        UserName = "admin",
        UserPassword = "admin"
    };

    private readonly Mock<MailingService.ExternalPackage.ISmtpClient> _smtpClientMock;
    private readonly MailingService _mailingService;

    public Examples()
    {
        _smtpClientMock = new Mock<MailingService.ExternalPackage.ISmtpClient>();
        _mailingService = new MailingService(_smtpClientMock.Object, Options.Create(Credentials));
    }

    [Fact]
    public void Builder_HelpsToHideSomePropsFromUserInterface()
    {
        _mailingService.SendMail(email
            => email
                .From(SomeCoolGuyEmail).To(SomeCoolGirlEmail)
                .WithSubject(SomeVeryInterestingTopic)
                .WithBody(SomeBody));

        _smtpClientMock.Verify(
            p => p.Send(
                It.Is<MailingService.ExternalPackage.Email>(
                    e => e.From == SomeCoolGuyEmail
                         && e.To == SomeCoolGirlEmail
                         && e.Subject == SomeVeryInterestingTopic
                         && e.Body == SomeBody
                         && e.Certificate == Credentials.Certificate
                         && e.UserName == Credentials.UserName
                         && e.UserPassword == Credentials.UserPassword)),
            Times.Once);
    }
}