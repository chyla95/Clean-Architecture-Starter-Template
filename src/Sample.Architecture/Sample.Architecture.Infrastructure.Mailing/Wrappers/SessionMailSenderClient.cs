using MailKit.Net.Smtp;
using MailKit.Security;
using Sample.Architecture.Application.Mailing.Enums;
using Sample.Architecture.Application.Mailing.Options;
using System.Net;

namespace Sample.Architecture.Infrastructure.Mailing.Wrappers;
internal class SessionMailSenderClient : SmtpClient, ISessionMailSenderClient
{
    private MailSenderClientOptions? _mailSenderClientOptions;

    public async Task CreateSessionAsync(MailSenderClientOptions mailingSenderOptions, CancellationToken cancellationToken = default)
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

        _mailSenderClientOptions = mailingSenderOptions;
    }

    public async Task RefreshSessionAsync(CancellationToken cancellationToken = default)
    {
        if (_mailSenderClientOptions is null) throw new InvalidOperationException("Cannot refresh a non-existent session");

        SecureSocketOptions secureSocketOptions = ConfigureSecureSocketOptions(_mailSenderClientOptions.EncryptionType);
        await ConnectAsync(
            _mailSenderClientOptions.HostAddress,
            _mailSenderClientOptions.PortNumber,
            secureSocketOptions,
            cancellationToken
        );

        ICredentials? credentials = ConfigureCredentials(
            _mailSenderClientOptions.Username,
            _mailSenderClientOptions.Password
        );
        if (credentials is not null) await AuthenticateAsync(credentials, cancellationToken);
    }

    public async Task ClearSessioAsync(CancellationToken cancellationToken = default)
    {
        _mailSenderClientOptions = null;
        await DisconnectAsync(true, cancellationToken);
    }

    private static SecureSocketOptions ConfigureSecureSocketOptions(MailEncryptionType mailEncryptionType)
    {
        SecureSocketOptions secureSocketOptions = mailEncryptionType switch
        {
            MailEncryptionType.None => SecureSocketOptions.None,
            MailEncryptionType.OptionalTls => SecureSocketOptions.StartTls,
            MailEncryptionType.MandatoryTls => SecureSocketOptions.SslOnConnect,
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
