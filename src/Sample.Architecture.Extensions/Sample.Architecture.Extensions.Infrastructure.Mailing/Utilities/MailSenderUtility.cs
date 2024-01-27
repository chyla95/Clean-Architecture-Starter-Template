using Sample.Architecture.Extensions.Application.Mailing.Models;
using Sample.Architecture.Extensions.Application.Mailing.Utilities;
using Sample.Architecture.Extensions.Infrastructure.Mailing.Factories;
using Sample.Architecture.Extensions.Infrastructure.Mailing.Wrappers;

namespace Sample.Architecture.Extensions.Infrastructure.Mailing.Utilities;
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
