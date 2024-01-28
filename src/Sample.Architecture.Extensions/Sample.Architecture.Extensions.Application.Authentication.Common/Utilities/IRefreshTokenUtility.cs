namespace Sample.Architecture.Extensions.Application.Authentication.Common.Utilities;
public interface IRefreshTokenUtility
{
    string GenerateRefreshToken(int tokenLength = 32);
}
