using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Authentication.Jwt.Options;
using Sample.Api.Authentication.Jwt.Services;
using Sample.Api.Authentication.Jwt.Strategies;

namespace Sample.Api.Authentication.Jwt.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtGenerator<TSigningCredentialsCreationStrategy>(this IServiceCollection services, string jwtGeneratorOptionsSectionName)
        where TSigningCredentialsCreationStrategy : class, IJwtGeneratorSigningCredentialsCreationStrategy
    {
        services.AddOptions<JwtGeneratorOptions>().BindConfiguration(jwtGeneratorOptionsSectionName);
        services.AddScoped<IJwtGeneratorSigningCredentialsCreationStrategy, TSigningCredentialsCreationStrategy>();
        services.AddScoped<IJwtGeneratorService, JwtGeneratorService>();

        return services;
    }
}
