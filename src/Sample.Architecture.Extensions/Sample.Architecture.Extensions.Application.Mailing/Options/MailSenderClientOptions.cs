using Sample.Architecture.Extensions.Application.Mailing.Enums;

namespace Sample.Architecture.Extensions.Application.Mailing.Options;
public record MailSenderClientOptions
{
    public required string Identifier { get; init; }
    public required bool IsDefault { get; init; }
    public required string HostAddress { get; init; }
    public required int PortNumber { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
    public required MailEncryptionType EncryptionType { get; init; }
}
