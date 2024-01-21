using Sample.Architecture.Application.Mailing.Enums;

namespace Sample.Architecture.Application.Mailing.Options;

public sealed record MailSenderOptions 
{
    public required MailSenderClientOptions DefaultMailSenderClientOptions { get; init; }
    public required IEnumerable<MailSenderClientOptions> OtherMailSenderClientsOptions { get; init; }
}