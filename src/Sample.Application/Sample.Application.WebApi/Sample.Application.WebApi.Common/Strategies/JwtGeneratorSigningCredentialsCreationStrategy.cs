using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Options;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Strategies;
using Sample.Architecture.Extensions.Application.Common.Abstractions.Utilities;
using System.Security.Cryptography;

namespace Sample.Application.WebApi.Common.Strategies;
public sealed class JwtGeneratorSigningCredentialsCreationStrategy(IOptionsMonitor<JwtGeneratorOptions> jwtGeneratorOptionsMonitor, IFileUtility fileUtility)
    : IJwtGeneratorSigningCredentialsCreationStrategy, IDisposable
{
    private readonly IOptionsMonitor<JwtGeneratorOptions> _jwtGeneratorOptionsMonitor = jwtGeneratorOptionsMonitor;
    private readonly IFileUtility _fileUtility = fileUtility;

    private static RSA? _rsaFromPrivateKey;

    public async Task<SigningCredentials> GenerateSigningCredentialsAsync(CancellationToken cancellationToken = default)
    {
        _rsaFromPrivateKey ??= await GetRsaFromPemFile(
            _fileUtility,
            _jwtGeneratorOptionsMonitor.CurrentValue.PrivateKeyFilePath,
            _jwtGeneratorOptionsMonitor.CurrentValue.PrivateKeyFilePassword,
            cancellationToken);

        SigningCredentials privateSigningCredentials = GetSigningCredentials(_rsaFromPrivateKey, _jwtGeneratorOptionsMonitor.CurrentValue.SecurityAlgorithm);
        return privateSigningCredentials;
    }

    private static async Task<RSA> GetRsaFromPemFile(IFileUtility fileUtility, string filePath, string? password = null, CancellationToken cancellationToken = default)
    {
        string fileContent = await fileUtility.ReadAllTextAsync(filePath, cancellationToken);

        RSA rsa = RSA.Create();
        if (password is null) rsa.ImportFromPem(fileContent);
        else rsa.ImportFromEncryptedPem(fileContent, password);

        return rsa;
    }

    private static SigningCredentials GetSigningCredentials(RSA rsa, string securityAlgorithm)
    {
        RsaSecurityKey rsaSecurityKey = new(rsa);
        CryptoProviderFactory cryptoProviderFactory = new()
        {
            CacheSignatureProviders = false
        };
        SigningCredentials signingCredentials = new(rsaSecurityKey, securityAlgorithm)
        {
            CryptoProviderFactory = cryptoProviderFactory
        };

        return signingCredentials;
    }

    public void Dispose()
    {
        if (_rsaFromPrivateKey is not null)
        {
            _rsaFromPrivateKey?.Dispose();
            _rsaFromPrivateKey = null;
        }
    }
}