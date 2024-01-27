using Microsoft.Extensions.Options;
using Sample.Architecture.Extensions.Application.Mailing.Options;
using Sample.Architecture.Extensions.Infrastructure.Mailing.Wrappers;
using System.Diagnostics.CodeAnalysis;

namespace Sample.Architecture.Extensions.Infrastructure.Mailing.Factories;
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

        IEnrichedMailSenderClient enrichedMailSenderClient = await CreateMailSenderClientFromOptionsAsync(mso => mso.IsDefault == true, cancellationToken);
        return enrichedMailSenderClient;
    }

    public async Task<IEnrichedMailSenderClient> GetMailSenderClientAsync(string identifier, CancellationToken cancellationToken = default)
    {
        IEnrichedMailSenderClient? mailSenderClient = _mailSenderClients.SingleOrDefault(sc => sc.Identifier == identifier);
        if (mailSenderClient is not null) return mailSenderClient;

        IEnrichedMailSenderClient enrichedMailSenderClient = await CreateMailSenderClientFromOptionsAsync(mso => mso.Identifier == identifier, cancellationToken);
        return enrichedMailSenderClient;
    }

    private async Task<IEnrichedMailSenderClient> CreateMailSenderClientFromOptionsAsync(Func<MailSenderClientOptions, bool> optionsPredicate, CancellationToken cancellationToken = default)
    {
        IEnumerable<MailSenderClientOptions> mailingSendersOptions = _mailSenderOptionsMonitor.CurrentValue.MailSenderClients;
        MailSenderClientOptions? mailingSenderOptions = mailingSendersOptions.SingleOrDefault(optionsPredicate);
        if (mailingSenderOptions is null) throw new NullReferenceException($"Could not find options that would meet specified conditions, to create {nameof(IEnrichedMailSenderClient)}");

        EnrichedMailSenderClient mailSenderClient = new(mailingSenderOptions.Identifier, mailingSenderOptions.IsDefault);
        await mailSenderClient.CreateSessionAsync(mailingSenderOptions, cancellationToken);
        bool wasMailSenderClientAdded = _mailSenderClients.Add(mailSenderClient);
        if (!wasMailSenderClientAdded) throw new InvalidOperationException($"Could not add '{nameof(mailSenderClient)}' to '{nameof(_mailSenderClients)}' collection");

        // Some Antiviruses may cause problems with "certificate revocation" for some SMTP servers,
        // to bypass this, set "CheckCertificateRevocation" to "false", like follows:
        // mailSenderClient.CheckCertificateRevocation = false;

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