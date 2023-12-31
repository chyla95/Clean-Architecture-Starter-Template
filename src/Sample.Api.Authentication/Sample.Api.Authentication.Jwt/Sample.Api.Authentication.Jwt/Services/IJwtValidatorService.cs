using Microsoft.IdentityModel.Tokens;

namespace Sample.Api.Authentication.Jwt.Services;
public interface IJwtValidatorService
{
    Task<bool> IsTokenValidAsync(string token, CancellationToken cancellationToken = default);
    Task<TokenValidationParameters> GenerateTokenValidationParametersAsync(CancellationToken cancellationToken = default);
}