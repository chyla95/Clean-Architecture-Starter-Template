using Microsoft.Extensions.Options;
using Sample.Architecture.Application.Mailing.Options;
using Sample.Architecture.Infrastructure.Mailing.Wrappers;
using System.Diagnostics.CodeAnalysis;

namespace Sample.Architecture.Infrastructure.Mailing.Factories;
internal sealed class MailSenderClientFactory(IOptionsMonitor<MailSenderOptions> mailSenderOptionsMonitor) : IDisposable, IMailSenderClientFactory
{
    private bool _isDisposed = false;
    private bool _isDisposing = false;

    private readonly IOptionsMonitor<MailSenderOptions> _mailSenderOptionsMonitor = mailSenderOptionsMonitor;
    private readonly HashSet<IEnrichedMailSenderClient> _mailSenderClients = new(new EnrichedMailSenderClientEqualityComparer());

    public async Task<IEnrichedMailSenderClient> GetMailSenderClientAsync(CancellationToken cancellationToken = default)
    {
        IEnrichedMailSenderClient? mailSenderClient = _mailSenderClients.SingleOrDefault(sc => sc.IsDefault == true);
        if (mailSenderClient is not null) return mailSenderClient;

        MailSenderClientOptions mailingSenderOptions = _mailSenderOptionsMonitor.CurrentValue.DefaultMailSenderClientOptions;
        mailSenderClient = await CreateMailSenderClientAsync(mailingSenderOptions, true, cancellationToken);

        bool wasMailSenderClientAdded = _mailSenderClients.Add(mailSenderClient);
        if (!wasMailSenderClientAdded) throw new InvalidOperationException($"Could not add '{nameof(mailSenderClient)}' to '{nameof(_mailSenderClients)}' collection");

        return mailSenderClient;
    }

    public async Task<IEnrichedMailSenderClient> GetMailSenderClientAsync(string identifier, CancellationToken cancellationToken = default)
    {
        IEnrichedMailSenderClient? mailSenderClient = _mailSenderClients.SingleOrDefault(sc => sc.Identifier == identifier);
        if (mailSenderClient is not null) return mailSenderClient;

        IEnumerable<MailSenderClientOptions> mailingSendersOptions = _mailSenderOptionsMonitor.CurrentValue.OtherMailSenderClientsOptions;
        MailSenderClientOptions? mailingSenderOptions = mailingSendersOptions.SingleOrDefault(mso => mso.Identifier == identifier);
        if (mailingSenderOptions is null) throw new NullReferenceException($"There is no matching configuration for '{identifier}'");

        mailSenderClient = await CreateMailSenderClientAsync(mailingSenderOptions, false, cancellationToken);
        bool wasMailSenderClientAdded = _mailSenderClients.Add(mailSenderClient);
        if (!wasMailSenderClientAdded) throw new InvalidOperationException($"Could not add '{nameof(mailSenderClient)}' to '{nameof(_mailSenderClients)}' collection");

        return mailSenderClient;
    }

    private static async Task<IEnrichedMailSenderClient> CreateMailSenderClientAsync(MailSenderClientOptions mailingSenderOptions, bool isDefault = false, CancellationToken cancellationToken = default)
    {
        // Some Antiviruses may cause problems with "certificate revocation" for some SMTP servers,
        // to bypass this, set "CheckCertificateRevocation" to "false", like follows:
        // mailSenderClient.CheckCertificateRevocation = false;

        EnrichedMailSenderClient mailSenderClient = isDefault
            ? new(mailingSenderOptions.Identifier, isDefault)
            : new(mailingSenderOptions.Identifier);

        await mailSenderClient.CreateSessionAsync(mailingSenderOptions, cancellationToken);

        return mailSenderClient;
    }

    public void Dispose()
    {
        if (_isDisposed || _isDisposing) return;

        _isDisposing = true;
        foreach (IEnrichedMailSenderClient mailSenderClient in _mailSenderClients)
        {
            mailSenderClient.Dispose();
        }
        _isDisposed = true;
    }

    private class EnrichedMailSenderClientEqualityComparer : IEqualityComparer<IEnrichedMailSenderClient>
    {
        public bool Equals(IEnrichedMailSenderClient? x, IEnrichedMailSenderClient? y)
        {
            if (ReferenceEquals(x, y)) return true;
            return x?.Identifier == y?.Identifier;
        }

        public int GetHashCode([DisallowNull] IEnrichedMailSenderClient obj)
            => obj.Identifier.GetHashCode();
    }
}