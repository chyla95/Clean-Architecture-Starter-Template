using MailKit.Net.Smtp;
using Sample.Architecture.Application.Mailing.Options;

namespace Sample.Architecture.Infrastructure.Mailing.Wrappers;
internal interface ISessionSmtpClient : ISmtpClient
{
    Task CreateSessionAsync(DefaultMailingSenderOptions mailingSenderOptions, CancellationToken cancellationToken = default);
    Task RefreshSessionAsync(CancellationToken cancellationToken = default);
    Task ClearSessioAsync(CancellationToken cancellationToken = default);
}