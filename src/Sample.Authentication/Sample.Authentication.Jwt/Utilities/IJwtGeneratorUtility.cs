namespace Sample.Authentication.Jwt.Utilities;
public interface IJwtGeneratorUtility
{
    Task<string> GenerateTokenAsync(string userId, CancellationToken cancellationToken = default);
}