using Sample.Architecture.Extensions.Application.Mailing.Enums;
using Sample.Architecture.Extensions.Application.Mailing.Models;

namespace Sample.Architecture.Extensions.Application.Mailing.Builders;
public sealed class MailMessageBuilder
{
    private readonly MailMessageModel _mailMessage = new();

    public MailMessageBuilder SetSubject(string subject)
    {
        if(!string.IsNullOrWhiteSpace(_mailMessage.Subject)) throw new InvalidOperationException($"Field: '{nameof(_mailMessage.Subject)}' cannot be set twice");

        _mailMessage.Subject = subject;
        return this;
    }

    public MailMessageBuilder SetBody(string value, MailBodyType mailContentType = MailBodyType.Text)
    {
        if (_mailMessage.Body is not null) throw new InvalidOperationException($"Field: '{nameof(_mailMessage.Body)}' cannot be set twice");

        _mailMessage.Body = new MailBodyModel(value, mailContentType);
        return this;
    }

    public MailMessageBuilder AddSender(string address, string? name = null)
    {
        _mailMessage.Senders.Add(new MailAddressModel(name ?? address, address));
        return this;
    }

    public MailMessageBuilder AddRecipient(string address, string? name = null)
    {
        _mailMessage.Recipients.Add(new MailAddressModel(name ?? address, address));
        return this;
    }

    public MailMessageBuilder AddCcRecipient(string address, string? name = null)
    {
        _mailMessage.CcRecipients.Add(new MailAddressModel(name ?? address, address));
        return this;
    }

    public MailMessageBuilder AddBccRecipient(string address, string? name = null)
    {
        _mailMessage.BccRecipients.Add(new MailAddressModel(name ?? address, address));
        return this;
    }

    public MailMessageBuilder AddAttachment(Stream fileStream, string name)
    {
        _mailMessage.Attachments.Add(new MailAttachmentModel(fileStream, name));
        return this;
    }

    public MailMessageModel CreateMailMessage()
    {
        if (_mailMessage.Senders.Count < 1) throw new InvalidOperationException("There should be at least one message sender defined");
        if (_mailMessage.Recipients.Count < 1) throw new InvalidOperationException("There should be at least one message recipent defined");

        return _mailMessage;
    }
}
