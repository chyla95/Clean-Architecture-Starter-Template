using Microsoft.IdentityModel.Tokens;

namespace Sample.Authentication.Jwt.Strategies;
public interface IJwtGeneratorSigningCredentialsCreationStrategy
{
    Task<SigningCredentials> GenerateSigningCredentialsAsync(CancellationToken cancellationToken = default);
}
