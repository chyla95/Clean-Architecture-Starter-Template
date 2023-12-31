namespace Sample.Api.Authentication.Jwt.Options;
public sealed record JwtGeneratorOptions
{
    public required string SecurityAlgorithm { get; init; }
    public required string PrivateKeyFilePath { get; init; }
    public required string? PrivateKeyFilePassword { get; init; }
    public required IEnumerable<string> Issuers { get; init; }
    public required IEnumerable<string> Audiences { get; init; }
    public TimeSpan? ValidAfter { get; init; }
    public TimeSpan? ExpiredAfter { get; init; }
}