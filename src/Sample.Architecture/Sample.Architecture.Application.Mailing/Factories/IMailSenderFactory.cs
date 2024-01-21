using Sample.Architecture.Application.Mailing.Utilities;

namespace Sample.Architecture.Application.Mailing.Factories;
public interface IMailSenderFactory
{
    IMailSenderUtility GetMailSenderUtility();
    IMailSenderUtility GetMailSenderUtility(string mailSenderName);
}
