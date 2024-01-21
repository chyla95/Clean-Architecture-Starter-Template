using Sample.Architecture.Application.Mailing.Models;

namespace Sample.Architecture.Infrastructure.Mailing.Wrappers;
internal interface IEnrichedMailSenderClient : ISessionMailSenderClient
{
    string Identifier { get; }
    bool IsDefault { get; }

    Task SendAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default);
}
