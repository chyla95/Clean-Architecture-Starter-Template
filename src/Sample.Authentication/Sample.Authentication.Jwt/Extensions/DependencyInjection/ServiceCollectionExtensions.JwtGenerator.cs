using Microsoft.Extensions.DependencyInjection;
using Sample.Authentication.Jwt.Constants;
using Sample.Authentication.Jwt.Options;
using Sample.Authentication.Jwt.Utilities;
using Sample.Authentication.Jwt.Strategies;

namespace Sample.Authentication.Jwt.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtGenerator<TSigningCredentialsCreationStrategy>(this IServiceCollection services)
        where TSigningCredentialsCreationStrategy : class, IJwtGeneratorSigningCredentialsCreationStrategy
    {
        services.AddOptions<JwtGeneratorOptions>().BindConfiguration(DefaultAppSettingsKeys.JwtGenerator);
        services.AddScoped<IJwtGeneratorSigningCredentialsCreationStrategy, TSigningCredentialsCreationStrategy>();
        services.AddScoped<IJwtGeneratorUtility, JwtGeneratorUtility>();

        return services;
    }
}
