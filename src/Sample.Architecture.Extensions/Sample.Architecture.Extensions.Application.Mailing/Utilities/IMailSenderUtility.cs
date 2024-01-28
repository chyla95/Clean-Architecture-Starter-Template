using Sample.Architecture.Extensions.Application.Mailing.Models;

namespace Sample.Architecture.Extensions.Application.Mailing.Utilities;
public interface IMailSenderUtility
{
    Task SendMessageAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default);
}
