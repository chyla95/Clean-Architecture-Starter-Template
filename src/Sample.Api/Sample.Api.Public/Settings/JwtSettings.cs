namespace Sample.Api.Public.Settings;

internal sealed class JwtSettings
{
    public const string SettingsSectionName = "JwtSettings";

    public required string Secret { get; init; }
}
