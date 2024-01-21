using Sample.Architecture.Infrastructure.Mailing.Wrappers;

namespace Sample.Architecture.Infrastructure.Mailing.Factories;
internal interface IMailSenderClientFactory : IDisposable
{
    Task<IEnrichedMailSenderClient> GetMailSenderClientAsync(CancellationToken cancellationToken = default);
    Task<IEnrichedMailSenderClient> GetMailSenderClientAsync(string identifier, CancellationToken cancellationToken = default);
}