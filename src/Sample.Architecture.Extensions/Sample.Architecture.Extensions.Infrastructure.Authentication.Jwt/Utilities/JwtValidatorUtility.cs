using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Options;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Strategies;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Sample.Architecture.Extensions.Infrastructure.Authentication.Jwt.Utilities;
internal sealed class JwtValidatorUtility(
    IJwtValidatorSigningCredentialsCreationStrategy jwtValidatorSigningCredentialsCreationStrategy,
    IOptionsMonitor<JwtValidatorOptions> jwtValidatorOptionsMonitor
    ) : IJwtValidatorUtility
{
    private readonly IJwtValidatorSigningCredentialsCreationStrategy _jwtValidatorSigningCredentialsCreationStrategy = jwtValidatorSigningCredentialsCreationStrategy;
    private readonly IOptionsMonitor<JwtValidatorOptions> _jwtValidatorOptionsMonitor = jwtValidatorOptionsMonitor;

    public async Task<bool> IsTokenValidAsync(string token, CancellationToken cancellationToken = default)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        if (!jwtSecurityTokenHandler.CanValidateToken) throw new InvalidOperationException("JwtSecurityTokenHandler cannot validate tokens in its current state.");

        TokenValidationParameters tokenValidationParameters = await GenerateTokenValidationParametersAsync(cancellationToken);
        TokenValidationResult tokenValidationResult = await jwtSecurityTokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

        bool isTokenValid = tokenValidationResult.IsValid;
        return isTokenValid;
    }

    public async Task<bool> CanTokenBeRefreshedAsync(string token, CancellationToken cancellationToken = default)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        if (!jwtSecurityTokenHandler.CanValidateToken) throw new InvalidOperationException("JwtSecurityTokenHandler cannot validate tokens in its current state.");

        TokenValidationParameters tokenValidationParameters = await GenerateTokenValidationParametersAsync(cancellationToken);
        tokenValidationParameters.ValidateLifetime = false;

        TokenValidationResult tokenValidationResult = await jwtSecurityTokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

        bool isTokenValid = tokenValidationResult.IsValid;
        return isTokenValid;
    }

    public async Task<TokenValidationParameters> GenerateTokenValidationParametersAsync(CancellationToken cancellationToken = default)
    {
        SigningCredentials signingCredentials = await _jwtValidatorSigningCredentialsCreationStrategy.GenerateSigningCredentialsAsync(cancellationToken);
        RSAParameters rsaParameters = GetRSAParameters(signingCredentials);

        JwtValidatorOptions jwtGeneratorOptions = _jwtValidatorOptionsMonitor.CurrentValue;
        TokenValidationParameters tokenValidationParameters = new()
        {
            IssuerSigningKey = new RsaSecurityKey(rsaParameters),
            ValidateIssuerSigningKey = jwtGeneratorOptions.ShouldValidateSigningKey,
            ValidateLifetime = jwtGeneratorOptions.ShouldValidateLifetime,
            ValidateIssuer = jwtGeneratorOptions.ValidIssuers.Any(),
            ValidateAudience = jwtGeneratorOptions.ValidAudiences.Any(),
            ValidAlgorithms = [jwtGeneratorOptions.SecurityAlgorithm],
            ValidIssuers = jwtGeneratorOptions.ValidIssuers,
            ValidAudiences = jwtGeneratorOptions.ValidAudiences,
            ClockSkew = jwtGeneratorOptions.ClockSkew ?? TimeSpan.FromSeconds(5),
        };

        return tokenValidationParameters;
    }

    private static RSAParameters GetRSAParameters(SigningCredentials signingCredentials)
    {
        if (signingCredentials.Key is not RsaSecurityKey rsaSecurityKey) throw new InvalidOperationException("The key is not an RsaSecurityKey.");

        RSAParameters rsaParameters = rsaSecurityKey.Rsa.ExportParameters(false);
        return rsaParameters;
    }
}