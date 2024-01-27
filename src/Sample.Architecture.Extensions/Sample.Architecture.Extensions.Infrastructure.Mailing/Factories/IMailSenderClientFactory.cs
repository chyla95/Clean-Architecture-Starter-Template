using Sample.Architecture.Extensions.Infrastructure.Mailing.Wrappers;

namespace Sample.Architecture.Extensions.Infrastructure.Mailing.Factories;
internal interface IMailSenderClientFactory : IDisposable
{
    Task<IEnrichedMailSenderClient> GetMailSenderClientAsync(CancellationToken cancellationToken = default);
    Task<IEnrichedMailSenderClient> GetMailSenderClientAsync(string identifier, CancellationToken cancellationToken = default);
}