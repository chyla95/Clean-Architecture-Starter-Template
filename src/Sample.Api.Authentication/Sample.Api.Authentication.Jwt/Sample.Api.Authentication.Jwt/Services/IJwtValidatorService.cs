using Microsoft.IdentityModel.Tokens;

namespace Sample.Api.Authentication.Jwt.Services;
public interface IJwtValidatorService
{
    /// <summary>
    /// Checks is the token is valid.
    /// </summary>
    Task<bool> IsTokenValidAsync(string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks is the token is valid, but skpis validating the Lifetime of the token.
    /// </summary>
    Task<bool> CanTokenBeRefreshedAsync(string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates TokenValidationParameters, based on the service configuration.
    /// </summary>
    Task<TokenValidationParameters> GenerateTokenValidationParametersAsync(CancellationToken cancellationToken = default);
}