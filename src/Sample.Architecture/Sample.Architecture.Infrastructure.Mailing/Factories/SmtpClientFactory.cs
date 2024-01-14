using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using Sample.Architecture.Application.Mailing.Enums;
using Sample.Architecture.Application.Mailing.Options;
using System.Net;


namespace Sample.Architecture.Infrastructure.Mailing.Factories;
internal sealed class SmtpClientFactory(ISmtpClient smtpClient, IOptionsMonitor<SmtpClientOptions> smtpClientOptionsMonitor) : IAsyncDisposable
{
    private bool _isDisposed = false;

    private readonly IOptionsMonitor<SmtpClientOptions> _smtpClientOptionsMonitor = smtpClientOptionsMonitor;
    private readonly ISmtpClient _smtpClient = smtpClient;

    public async Task<ISmtpClient> GetSmtpClientAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, nameof(SmtpClientFactory));

        SmtpClientOptions smtpClientOptions = _smtpClientOptionsMonitor.CurrentValue;

        // Some Antiviruses may cause problems with "certificate revocation",
        // to bypass this, set "CheckCertificateRevocation" to "false", like follows:
        // _smtpClient.CheckCertificateRevocation = false;

        SecureSocketOptions secureSocketOptions = smtpClientOptions.EncryptionType switch
        {
            MailEncryptionType.None => SecureSocketOptions.None,
            MailEncryptionType.OpportunisticTls => SecureSocketOptions.StartTls,
            MailEncryptionType.ForcedTls => SecureSocketOptions.SslOnConnect,
            _ => throw new InvalidOperationException($"Unsupported {nameof(MailEncryptionType)}")
        };

        await _smtpClient.ConnectAsync(
            smtpClientOptions.HostAddress,
            smtpClientOptions.PortNumber,
            secureSocketOptions,
            cancellationToken
            );

        if (!string.IsNullOrWhiteSpace(smtpClientOptions.Password) || !string.IsNullOrWhiteSpace(smtpClientOptions.Username))
        {
            ICredentials credentials = new NetworkCredential
            {
                UserName = smtpClientOptions.Username,
                Password = smtpClientOptions.Password
            };

            await _smtpClient.AuthenticateAsync(credentials, cancellationToken);
        }

        return _smtpClient;
    }

    public async ValueTask DisposeAsync()
    {
        if (_isDisposed) return;

        await _smtpClient.DisconnectAsync(true);

        _smtpClient.Dispose();
        _isDisposed = true;
    }
}