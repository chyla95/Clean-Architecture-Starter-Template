using Sample.Architecture.Application.Mailing.Models;

namespace Sample.Architecture.Application.Mailing.Utilities;
public interface IMailSenderUtility
{
    Task SendMessageAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default);
}
