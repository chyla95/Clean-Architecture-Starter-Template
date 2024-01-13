﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.Authentication.Jwt.Options;
using Sample.Architecture.Application.Utilities;
using System.Security.Cryptography;

namespace Sample.Authentication.Jwt.Strategies;
public sealed class JwtValidatorSigningCredentialsCreationStrategy(IOptionsMonitor<JwtValidatorOptions> jwtValidatorOptionsMonitor, IFileUtility fileUtility)
    : IJwtValidatorSigningCredentialsCreationStrategy, IDisposable
{
    private readonly IOptionsMonitor<JwtValidatorOptions> _jwtValidatorOptionsMonitor = jwtValidatorOptionsMonitor;
    private readonly IFileUtility _fileUtility = fileUtility;

    private static RSA? _rsaFromPublicKey;

    public async Task<SigningCredentials> GenerateSigningCredentialsAsync(CancellationToken cancellationToken = default)
    {
        _rsaFromPublicKey ??= await GetRsaFromPemFile(
            _fileUtility,
            _jwtValidatorOptionsMonitor.CurrentValue.PublicKeyFilePath,
            cancellationToken);

        SigningCredentials privateSigningCredentials = GetSigningCredentials(_rsaFromPublicKey, _jwtValidatorOptionsMonitor.CurrentValue.SecurityAlgorithm);
        return privateSigningCredentials;
    }

    private static async Task<RSA> GetRsaFromPemFile(IFileUtility fileUtility, string filePath, CancellationToken cancellationToken = default)
    {
        string fileContent = await fileUtility.ReadAllTextAsync(filePath, cancellationToken);

        RSA rsa = RSA.Create();
        rsa.ImportFromPem(fileContent);

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
        if (_rsaFromPublicKey is not null)
        {
            _rsaFromPublicKey?.Dispose();
            _rsaFromPublicKey = null;
        }
    }
}