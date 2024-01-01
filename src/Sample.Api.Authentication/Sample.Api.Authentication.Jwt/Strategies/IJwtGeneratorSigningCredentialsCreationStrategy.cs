using Microsoft.IdentityModel.Tokens;

namespace Sample.Api.Authentication.Jwt.Strategies;
public interface IJwtGeneratorSigningCredentialsCreationStrategy
{
    Task<SigningCredentials> GenerateSigningCredentialsAsync(CancellationToken cancellationToken = default);
}
