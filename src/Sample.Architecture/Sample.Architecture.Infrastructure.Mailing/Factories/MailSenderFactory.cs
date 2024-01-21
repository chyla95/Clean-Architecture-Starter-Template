using Sample.Architecture.Application.Mailing.Factories;
using Sample.Architecture.Application.Mailing.Utilities;
using Sample.Architecture.Infrastructure.Mailing.Utilities;

namespace Sample.Architecture.Infrastructure.Mailing.Factories;
internal sealed class MailSenderFactory(IMailSenderClientFactory mailSenderClientFactory) : IMailSenderFactory
{
    private readonly IMailSenderClientFactory _mailSenderClientFactory = mailSenderClientFactory;

    public IMailSenderUtility GetMailSenderUtility() 
        => new MailSenderUtility(_mailSenderClientFactory);

    public IMailSenderUtility GetMailSenderUtility(string identifier) 
        => new MailSenderUtility(_mailSenderClientFactory, identifier);
}
