using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Authentication.Jwt.Constants;
using Sample.Api.Authentication.Jwt.Options;
using Sample.Api.Authentication.Jwt.Utilities;
using Sample.Api.Authentication.Jwt.Strategies;

namespace Sample.Api.Authentication.Jwt.Extensions.DependencyInjection;
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
