using Microsoft.Extensions.Options;
using Sample.Architecture.Application.Mailing.Options;
using Sample.Architecture.Infrastructure.Mailing.Wrappers;
using System.Diagnostics.CodeAnalysis;

namespace Sample.Architecture.Infrastructure.Mailing.Factories;
internal sealed class SmtpClientFactory(IOptionsMonitor<MailingSenderOptions> smtpClientOptionsMonitor) : IDisposable
{
    private bool _isDisposed = false;
    private bool _isDisposing = false;

    private readonly IOptionsMonitor<MailingSenderOptions> _smtpClientOptionsMonitor = smtpClientOptionsMonitor;
    private readonly HashSet<IRichSmtpClient> _smtpClients = new(new RichSmtpClientEqualityComparer());

    public async Task<IRichSmtpClient> GetSmtpClientAsync(CancellationToken cancellationToken = default)
    {
        IRichSmtpClient? smtpClient = _smtpClients.SingleOrDefault(sc => sc.IsDefault == true);
        if (smtpClient is not null) return smtpClient;

        DefaultMailingSenderOptions mailingSenderOptions = _smtpClientOptionsMonitor.CurrentValue.DefaultMailingSender;
        smtpClient = await CreateSmtpClientAsync(mailingSenderOptions, null, cancellationToken);

        bool isSmtpClientAdded = _smtpClients.Add(smtpClient);
        if (!isSmtpClientAdded) throw new InvalidOperationException($"Could not add {nameof(smtpClient)} to {nameof(_smtpClients)} collection");

        return smtpClient;
    }

    public async Task<IRichSmtpClient> GetSmtpClientAsync(string smtpClientName, CancellationToken cancellationToken = default)
    {
        IRichSmtpClient? smtpClient = _smtpClients.SingleOrDefault(sc => sc.Name == smtpClientName);
        if (smtpClient is not null) return smtpClient;

        IEnumerable<NamedMailingSendersOptions> mailingSendersOptions = _smtpClientOptionsMonitor.CurrentValue.NamedMailingSenders;
        NamedMailingSendersOptions? mailingSenderOptions = mailingSendersOptions.SingleOrDefault(mso => mso.Name == smtpClientName);
        if (mailingSenderOptions is null) throw new NullReferenceException($"There is no matching configuration for '{smtpClientName}' SMTP Client");

        smtpClient = await CreateSmtpClientAsync(mailingSenderOptions, smtpClientName, cancellationToken);
        bool isSmtpClientAdded = _smtpClients.Add(smtpClient);
        if (!isSmtpClientAdded) throw new InvalidOperationException($"Could not add {nameof(smtpClient)} to {nameof(_smtpClients)} collection");

        return smtpClient;
    }

    private static async Task<IRichSmtpClient> CreateSmtpClientAsync(DefaultMailingSenderOptions mailingSenderOptions, string? name = null, CancellationToken cancellationToken = default)
    {
        // Some Antiviruses may cause problems with "certificate revocation" for some SMTP servers,
        // to bypass this, set "CheckCertificateRevocation" to "false", like follows:
        // sessionSmtpClient.CheckCertificateRevocation = false;

        RichSmtpClient sessionSmtpClient = name is not null ? new(name) : new();
        await sessionSmtpClient.CreateSessionAsync(mailingSenderOptions, cancellationToken);

        return sessionSmtpClient;
    }

    public void Dispose()
    {
        if (_isDisposed || _isDisposing) return;

        _isDisposing = true;
        foreach (IRichSmtpClient smtpClient in _smtpClients)
        {
            smtpClient.Dispose();
        }
        _isDisposed = true;
    }

    private class RichSmtpClientEqualityComparer : IEqualityComparer<IRichSmtpClient>
    {
        public bool Equals(IRichSmtpClient? x, IRichSmtpClient? y)
        {
            if (ReferenceEquals(x, y)) return true;
            return (x?.Name == y?.Name) && (x?.IsDefault == y?.IsDefault);
        }

        public int GetHashCode([DisallowNull] IRichSmtpClient obj) => HashCode.Combine(obj.Name, obj.IsDefault);
    }
}