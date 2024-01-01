using Microsoft.IdentityModel.Tokens;

namespace Sample.Api.Authentication.Jwt.Strategies;
public interface IJwtValidatorSigningCredentialsCreationStrategy
{
    Task<SigningCredentials> GenerateSigningCredentialsAsync(CancellationToken cancellationToken = default);
}
