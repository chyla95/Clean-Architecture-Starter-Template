using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Authentication.Jwt.Constants;
using Sample.Api.Authentication.Jwt.Options;
using Sample.Api.Authentication.Jwt.Utilities;
using Sample.Api.Authentication.Jwt.Strategies;

namespace Sample.Api.Authentication.Jwt.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtValidator<TSigningCredentialsCreationStrategy>(this IServiceCollection services)
        where TSigningCredentialsCreationStrategy : class, IJwtValidatorSigningCredentialsCreationStrategy
    {
        services.AddOptions<JwtValidatorOptions>().BindConfiguration(AppSettingsKeyConstants.JwtValidator);
        services.AddScoped<IJwtValidatorSigningCredentialsCreationStrategy, TSigningCredentialsCreationStrategy>();
        services.AddScoped<IJwtValidatorUtility, JwtValidatorUtility>();

        // Other
        services.AddTransient<IJwtClaimsUtility, JwtClaimsUtility>();

        return services;
    }
}
