using Sample.Architecture.Application.Mailing.Models;

namespace Sample.Architecture.Infrastructure.Mailing.Wrappers;
internal interface IRichSmtpClient : ISessionSmtpClient
{
    string? Name { get; }
    bool? IsDefault { get; }

    Task SendAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default);
}
