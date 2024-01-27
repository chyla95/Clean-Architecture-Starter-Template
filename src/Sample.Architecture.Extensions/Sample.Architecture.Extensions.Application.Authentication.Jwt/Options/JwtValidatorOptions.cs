namespace Sample.Architecture.Extensions.Application.Authentication.Jwt.Options;
public sealed record JwtValidatorOptions
{
    public required bool ShouldValidateSigningKey { get; init; }
    public required bool ShouldValidateLifetime { get; init; }
    public required string SecurityAlgorithm { get; init; }
    public required string PublicKeyFilePath { get; init; }
    public required IEnumerable<string> ValidIssuers { get; init; }
    public required IEnumerable<string> ValidAudiences { get; init; }
    public TimeSpan? ClockSkew { get; init; }
}