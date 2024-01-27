using Microsoft.IdentityModel.Tokens;

namespace Sample.Architecture.Extensions.Application.Authentication.Jwt.Strategies;
public interface IJwtValidatorSigningCredentialsCreationStrategy
{
    Task<SigningCredentials> GenerateSigningCredentialsAsync(CancellationToken cancellationToken = default);
}
