using Microsoft.IdentityModel.Tokens;

namespace Sample.Authentication.Jwt.Strategies;
public interface IJwtValidatorSigningCredentialsCreationStrategy
{
    Task<SigningCredentials> GenerateSigningCredentialsAsync(CancellationToken cancellationToken = default);
}
