using MailKit;
using MimeKit;
using Sample.Architecture.Extensions.Application.Mailing.Enums;
using Sample.Architecture.Extensions.Application.Mailing.Models;

namespace Sample.Architecture.Extensions.Infrastructure.Mailing.Wrappers;
internal sealed class EnrichedMailSenderClient(string identifier) : SessionMailSenderClient, IEnrichedMailSenderClient
{
    private readonly string _identifier = identifier;
    private readonly bool _isDefault = false;

    public string Identifier => _identifier;
    public bool IsDefault => _isDefault;

    public EnrichedMailSenderClient(string identifier, bool isDefault) : this(identifier)
    {
        _isDefault = isDefault;
    }

    public async Task SendAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default)
    {
        MimeMessage mimeMessage = await MapMimeMessage(mailMessageModel);

        try
        {
            _ = await SendAsync(mimeMessage, cancellationToken);
        }
        catch (Exception exception) when (exception is ServiceNotConnectedException || exception is ServiceNotAuthenticatedException)
        {
            await RefreshSessionAsync(cancellationToken);
            _ = await SendAsync(mimeMessage, cancellationToken);
        }
    }

    private async static Task<MimeMessage> MapMimeMessage(MailMessageModel mailMessageModel)
    {
        MimeMessage mimeMessage = new();

        // Add message senders
        IEnumerable<MailboxAddress> sendersAddresses = mailMessageModel.Senders.Select(s => new MailboxAddress(s.Name, s.Address));
        mimeMessage.From.AddRange(sendersAddresses);

        // Add message recipients
        IEnumerable<MailboxAddress> recipientsAddresses = mailMessageModel.Recipients.Select(s => new MailboxAddress(s.Name, s.Address));
        mimeMessage.To.AddRange(recipientsAddresses);

        // Add message Cc
        IEnumerable<MailboxAddress> ccRecipients = mailMessageModel.CcRecipients.Select(s => new MailboxAddress(s.Name, s.Address));
        mimeMessage.Cc.AddRange(ccRecipients);

        // Add message Bcc
        IEnumerable<MailboxAddress> bccRecipients = mailMessageModel.BccRecipients.Select(s => new MailboxAddress(s.Name, s.Address));
        mimeMessage.Bcc.AddRange(bccRecipients);

        // Add message subject
        mimeMessage.Subject = mailMessageModel.Subject;

        BodyBuilder messageBodyBuilder = new();

        // Add message content
        switch (mailMessageModel.Body?.MailBodyType)
        {
            case MailBodyType.Text:
                messageBodyBuilder.TextBody = mailMessageModel.Body.Content;
                break;

            case MailBodyType.Html:
                messageBodyBuilder.HtmlBody = mailMessageModel.Body.Content;
                break;

            default:
                throw new InvalidOperationException($"Unsupported {nameof(MailBodyType)}");
        }

        // Add message attachments
        if (mailMessageModel.Attachments.Count > 0)
        {
            foreach (MailAttachmentModel attachment in mailMessageModel.Attachments)
            {
                await messageBodyBuilder.Attachments.AddAsync(attachment.Name, attachment.FileStream);
            }
        }

        mimeMessage.Body = messageBodyBuilder.ToMessageBody();
        return mimeMessage;
    }
}