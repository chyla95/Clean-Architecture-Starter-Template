using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.Api.Authentication.Jwt.Options;
using Sample.Api.Authentication.Jwt.Strategies;
using Sample.Architecture.Application.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sample.Api.Authentication.Jwt.Services;
internal sealed class JwtGeneratorService(
    IJwtGeneratorSigningCredentialsCreationStrategy jwtGeneratorSigningCredentialsCreationStrategy,
    IOptionsMonitor<JwtGeneratorOptions> jwtGeneratorOptionsMonitor,
    ITimeUtility timeUtility
    ) : IJwtGeneratorService
{
    private readonly IOptionsMonitor<JwtGeneratorOptions> _getGeneratorOptions = jwtGeneratorOptionsMonitor;
    private readonly IJwtGeneratorSigningCredentialsCreationStrategy _jwtGeneratorSigningCredentialsCreationStrategy = jwtGeneratorSigningCredentialsCreationStrategy;
    private readonly ITimeUtility _timeUtility = timeUtility;

    public async Task<string> GenerateTokenAsync(string subject, CancellationToken cancellationToken = default)
    {
        IEnumerable<Claim> claims = GenerateClaims(subject);
        SigningCredentials signingCredentials = await _jwtGeneratorSigningCredentialsCreationStrategy.GenerateSigningCredentialsAsync(cancellationToken);
        JwtSecurityToken jwt = new(
            claims: claims,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        if (!jwtSecurityTokenHandler.CanWriteToken) throw new InvalidOperationException("JwtSecurityTokenHandler cannot write tokens in its current state.");

        string tokenValue = jwtSecurityTokenHandler.WriteToken(jwt);
        return tokenValue;
    }

    private IEnumerable<Claim> GenerateClaims(string subject)
    {
        DateTimeOffset tokenGenerationTime = _timeUtility.GetUtcDateTimeOffset();
        JwtGeneratorOptions jwtGeneratorOptions = _getGeneratorOptions.CurrentValue;

        // Sub
        yield return new Claim(JwtRegisteredClaimNames.Sub, subject);

        // Iat
        yield return new Claim(JwtRegisteredClaimNames.Iat, tokenGenerationTime.ToUnixTimeSeconds().ToString());

        // Nbf
        TimeSpan? validAfter = jwtGeneratorOptions.ValidAfter;
        if (validAfter is not null) yield return new Claim(JwtRegisteredClaimNames.Nbf, (tokenGenerationTime + (validAfter ?? TimeSpan.Zero)).ToUnixTimeSeconds().ToString());

        // Exp
        TimeSpan? expiredAfter = jwtGeneratorOptions.ExpiredAfter;
        if (expiredAfter is not null) yield return new Claim(JwtRegisteredClaimNames.Exp, (tokenGenerationTime + (expiredAfter ?? TimeSpan.Zero)).ToUnixTimeSeconds().ToString());

        // Iss
        string? issuer = jwtGeneratorOptions.Issuer;
        if (!string.IsNullOrWhiteSpace(issuer)) yield return new Claim(JwtRegisteredClaimNames.Iss, issuer);

        // Aud
        IEnumerable<string> audiences = jwtGeneratorOptions.Audiences;
        if (audiences.Any())
        {
            IEnumerable<Claim> audienceClaims = audiences.Select(a => new Claim(JwtRegisteredClaimNames.Aud, a));
            foreach (Claim audienceClaim in audienceClaims) yield return audienceClaim;
        }
    }
}