using Sample.Architecture.Application.Mailing.Models;
using Sample.Architecture.Application.Mailing.Utilities;
using Sample.Architecture.Infrastructure.Mailing.Factories;
using Sample.Architecture.Infrastructure.Mailing.Wrappers;

namespace Sample.Architecture.Infrastructure.Mailing.Utilities;
internal sealed class MailSenderUtility(SmtpClientFactory smtpClientFactory) : IMailSenderUtility
{
    private readonly SmtpClientFactory _smtpClientFactory = smtpClientFactory;
    private readonly string? _mailSenderName;

    public MailSenderUtility(SmtpClientFactory smtpClientFactory, string mailSenderName) : this(smtpClientFactory)
    {
        _mailSenderName = mailSenderName;
    }

    public async Task SendMessageAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default)
    {
        IRichSmtpClient smtpClient = string.IsNullOrWhiteSpace(_mailSenderName)
            ? await _smtpClientFactory.GetSmtpClientAsync(cancellationToken)
            : await _smtpClientFactory.GetSmtpClientAsync(_mailSenderName, cancellationToken);

        await smtpClient.SendAsync(mailMessageModel, cancellationToken);
    }
}
