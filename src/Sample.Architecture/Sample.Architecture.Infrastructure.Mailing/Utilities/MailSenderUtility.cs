using Sample.Architecture.Application.Mailing.Models;
using Sample.Architecture.Application.Mailing.Utilities;
using MimeKit;
using Sample.Architecture.Application.Mailing.Enums;
using Sample.Architecture.Infrastructure.Mailing.Factories;
using MailKit.Net.Smtp;

namespace Sample.Architecture.Infrastructure.Mailing.Utilities;
internal sealed class MailSenderUtility(SmtpClientFactory smtpClientFactory) : IMailSenderUtility
{
    private readonly SmtpClientFactory _smtpClientFactory = smtpClientFactory;

    public async Task SendMessageAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default)
    {
        ISmtpClient smtpClient = await _smtpClientFactory.GetSmtpClientAsync(cancellationToken);

        MimeMessage mimeMessage = await BuildMimeMessage(mailMessageModel);
        await smtpClient.SendAsync(mimeMessage, cancellationToken);
    }

    private async static Task<MimeMessage> BuildMimeMessage(MailMessageModel mailMessageModel)
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
