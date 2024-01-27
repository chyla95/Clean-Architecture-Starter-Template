namespace Sample.Architecture.Application.Mailing.Options;

public sealed record MailSenderOptions 
{
    public required IEnumerable<MailSenderClientOptions> MailSenderClients { get; init; }
}