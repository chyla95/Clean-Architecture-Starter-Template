using Sample.Architecture.Application.Mailing.Enums;

namespace Sample.Architecture.Application.Mailing.Options;
public sealed record SmtpClientOptions
{
    public required string HostAddress { get; init; }
    public required int PortNumber { get; init; }
    public required MailEncryptionType EncryptionType { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
}
