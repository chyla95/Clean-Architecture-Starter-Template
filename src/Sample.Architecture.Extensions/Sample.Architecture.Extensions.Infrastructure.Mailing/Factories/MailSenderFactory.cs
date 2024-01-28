using Sample.Architecture.Extensions.Application.Mailing.Factories;
using Sample.Architecture.Extensions.Application.Mailing.Utilities;
using Sample.Architecture.Extensions.Infrastructure.Mailing.Utilities;

namespace Sample.Architecture.Extensions.Infrastructure.Mailing.Factories;
internal sealed class MailSenderFactory(IMailSenderClientFactory mailSenderClientFactory) : IMailSenderFactory
{
    private readonly IMailSenderClientFactory _mailSenderClientFactory = mailSenderClientFactory;

    public IMailSenderUtility GetMailSenderUtility() 
        => new MailSenderUtility(_mailSenderClientFactory);

    public IMailSenderUtility GetMailSenderUtility(string identifier) 
        => new MailSenderUtility(_mailSenderClientFactory, identifier);
}
