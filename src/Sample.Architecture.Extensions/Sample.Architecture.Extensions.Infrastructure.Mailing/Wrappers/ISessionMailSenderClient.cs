using MailKit.Net.Smtp;
using Sample.Architecture.Extensions.Application.Mailing.Options;

namespace Sample.Architecture.Extensions.Infrastructure.Mailing.Wrappers;
internal interface ISessionMailSenderClient : ISmtpClient
{
    Task CreateSessionAsync(MailSenderClientOptions mailingSenderOptions, CancellationToken cancellationToken = default);
    Task RefreshSessionAsync(CancellationToken cancellationToken = default);
    Task ClearSessioAsync(CancellationToken cancellationToken = default);
}