using Sample.Architecture.Application.Mailing.Enums;

namespace Sample.Architecture.Application.Mailing.Options;

public sealed record MailingSenderOptions
{
    public required DefaultMailingSenderOptions DefaultMailingSender { get; init; }
    public required IEnumerable<NamedMailingSendersOptions> NamedMailingSenders { get; init; }
}

public record NamedMailingSendersOptions : DefaultMailingSenderOptions
{
    public required string Name { get; init; }
}

public record DefaultMailingSenderOptions
{
    public required string HostAddress { get; init; }
    public required int PortNumber { get; init; }
    public required MailEncryptionType EncryptionType { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
}