namespace Creational.Builder.HiddenObject;

public interface IEmailBuilder
{
    IEmailBuilder From(string from);
    IEmailBuilder To(string to);
    IEmailBuilder WithSubject(string body);
    IEmailBuilder WithBody(string body);
}