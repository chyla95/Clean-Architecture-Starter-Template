using Sample.Architecture.Extensions.Application.Mailing.Utilities;

namespace Sample.Architecture.Extensions.Application.Mailing.Factories;
public interface IMailSenderFactory
{
    IMailSenderUtility GetMailSenderUtility();
    IMailSenderUtility GetMailSenderUtility(string identifier);
}
