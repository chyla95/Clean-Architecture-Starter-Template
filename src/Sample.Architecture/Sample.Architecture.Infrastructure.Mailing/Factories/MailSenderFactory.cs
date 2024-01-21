using Sample.Architecture.Application.Mailing.Factories;
using Sample.Architecture.Application.Mailing.Utilities;
using Sample.Architecture.Infrastructure.Mailing.Utilities;

namespace Sample.Architecture.Infrastructure.Mailing.Factories;
internal sealed class MailSenderFactory(SmtpClientFactory smtpClientFactory) : IMailSenderFactory
{
    private readonly SmtpClientFactory _smtpClientFactory = smtpClientFactory;

    public IMailSenderUtility GetMailSenderUtility() 
        => new MailSenderUtility(_smtpClientFactory);

    public IMailSenderUtility GetMailSenderUtility(string mailSenderName) 
        => new MailSenderUtility(_smtpClientFactory, mailSenderName);
}
