using Sample.Architecture.Application.Mailing.Models;
using Sample.Architecture.Application.Mailing.Utilities;
using Sample.Architecture.Infrastructure.Mailing.Factories;
using Sample.Architecture.Infrastructure.Mailing.Wrappers;

namespace Sample.Architecture.Infrastructure.Mailing.Utilities;
internal sealed class MailSenderUtility(IMailSenderClientFactory mailSenderClientFactory) : IMailSenderUtility
{
    private readonly IMailSenderClientFactory _mailSenderClientFactory = mailSenderClientFactory;
    private readonly string? _mailSenderClientIdentifier;

    public MailSenderUtility(IMailSenderClientFactory mailSenderClientFactory, string mailSenderClientIdentifier) : this(mailSenderClientFactory)
    {
        _mailSenderClientIdentifier = mailSenderClientIdentifier;
    }

    public async Task SendMessageAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default)
    {
        IEnrichedMailSenderClient mailSenderClient = string.IsNullOrWhiteSpace(_mailSenderClientIdentifier)
            ? await _mailSenderClientFactory.GetMailSenderClientAsync(cancellationToken)
            : await _mailSenderClientFactory.GetMailSenderClientAsync(_mailSenderClientIdentifier, cancellationToken);

        await mailSenderClient.SendAsync(mailMessageModel, cancellationToken);
    }
}
