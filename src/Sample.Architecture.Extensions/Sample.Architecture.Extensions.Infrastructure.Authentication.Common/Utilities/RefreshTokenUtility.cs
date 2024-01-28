using Sample.Architecture.Extensions.Application.Authentication.Common.Utilities;
using System.Security.Cryptography;

namespace Sample.Architecture.Extensions.Infrastructure.Authentication.Common.Utilities;
internal sealed class RefreshTokenUtility : IRefreshTokenUtility
{
    public string GenerateRefreshToken(int tokenLength = 32)
    {
        if(tokenLength < 32) throw new ArgumentException("Refresh token length must be at least 32 characters.", nameof(tokenLength));

        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

        byte[] randomNumber = new byte[tokenLength];
        randomNumberGenerator.GetBytes(randomNumber);
        string refreshToken = Convert.ToBase64String(randomNumber);

        return refreshToken;
    }
}
