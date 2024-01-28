using Sample.Architecture.Extensions.Application.Mailing.Models;

namespace Sample.Architecture.Extensions.Infrastructure.Mailing.Wrappers;
internal interface IEnrichedMailSenderClient : ISessionMailSenderClient
{
    string Identifier { get; }
    bool IsDefault { get; }

    Task SendAsync(MailMessageModel mailMessageModel, CancellationToken cancellationToken = default);
}
