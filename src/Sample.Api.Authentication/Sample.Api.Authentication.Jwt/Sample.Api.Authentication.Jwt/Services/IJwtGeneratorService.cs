namespace Sample.Api.Authentication.Jwt.Services;
public interface IJwtGeneratorService
{
    Task<string> GenerateTokenAsync(string userId, CancellationToken cancellationToken = default);
}