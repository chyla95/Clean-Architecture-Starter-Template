using MailKit.Net.Smtp;
using MailKit.Security;
using Sample.Architecture.Application.Mailing.Enums;
using Sample.Architecture.Application.Mailing.Options;
using System.Net;

namespace Sample.Architecture.Infrastructure.Mailing.Wrappers;
internal class SessionSmtpClient() : SmtpClient, ISessionSmtpClient
{
    private DefaultMailingSenderOptions? _sessionMailingSenderOptions;

    public async Task CreateSessionAsync(DefaultMailingSenderOptions mailingSenderOptions, CancellationToken cancellationToken = default)
    {
        SecureSocketOptions secureSocketOptions = ConfigureSecureSocketOptions(mailingSenderOptions.EncryptionType);
        await ConnectAsync(
            mailingSenderOptions.HostAddress,
            mailingSenderOptions.PortNumber,
            secureSocketOptions,
            cancellationToken
        );

        ICredentials? credentials = ConfigureCredentials(
            mailingSenderOptions.Username,
            mailingSenderOptions.Password
        );
        if (credentials is not null) await AuthenticateAsync(credentials, cancellationToken);

        _sessionMailingSenderOptions = mailingSenderOptions;
    }

    public async Task RefreshSessionAsync(CancellationToken cancellationToken = default)
    {
        if (_sessionMailingSenderOptions is null) throw new InvalidOperationException("Cannot refresh a non-existent session.");

        SecureSocketOptions secureSocketOptions = ConfigureSecureSocketOptions(_sessionMailingSenderOptions.EncryptionType);
        await ConnectAsync(
            _sessionMailingSenderOptions.HostAddress,
            _sessionMailingSenderOptions.PortNumber,
            secureSocketOptions,
            cancellationToken
        );

        ICredentials? credentials = ConfigureCredentials(
            _sessionMailingSenderOptions.Username,
            _sessionMailingSenderOptions.Password
        );
        if (credentials is not null) await AuthenticateAsync(credentials, cancellationToken);
    }

    public async Task ClearSessioAsync(CancellationToken cancellationToken = default)
    {
        _sessionMailingSenderOptions = null;
        await DisconnectAsync(true, cancellationToken);
    }

    private static SecureSocketOptions ConfigureSecureSocketOptions(MailEncryptionType mailEncryptionType)
    {
        SecureSocketOptions secureSocketOptions = mailEncryptionType switch
        {
            MailEncryptionType.None => SecureSocketOptions.None,
            MailEncryptionType.OpportunisticTls => SecureSocketOptions.StartTls,
            MailEncryptionType.ForcedTls => SecureSocketOptions.SslOnConnect,
            _ => throw new InvalidOperationException($"Unsupported {nameof(MailEncryptionType)}")
        };

        return secureSocketOptions;
    }

    private static ICredentials? ConfigureCredentials(string? username, string? password)
    {
        if (string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(username)) return null;

        ICredentials credentials = new NetworkCredential
        {
            UserName = username,
            Password = password
        };

        return credentials;
    }
}
