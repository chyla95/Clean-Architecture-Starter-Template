namespace Sample.Authentication.Common.Utilities;
public interface IRefreshTokenUtility
{
    string GenerateRefreshToken(int tokenLength = 32);
}
