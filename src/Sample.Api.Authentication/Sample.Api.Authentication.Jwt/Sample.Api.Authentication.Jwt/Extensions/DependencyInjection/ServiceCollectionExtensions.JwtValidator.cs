using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Authentication.Jwt.Constants;
using Sample.Api.Authentication.Jwt.Options;
using Sample.Api.Authentication.Jwt.Services;
using Sample.Api.Authentication.Jwt.Strategies;

namespace Sample.Api.Authentication.Jwt.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtValidator<TSigningCredentialsCreationStrategy>(this IServiceCollection services)
        where TSigningCredentialsCreationStrategy : class, IJwtValidatorSigningCredentialsCreationStrategy
    {
        services.AddOptions<JwtValidatorOptions>().BindConfiguration(DefaultAppSettingsKeys.JwtValidator);
        services.AddScoped<IJwtValidatorSigningCredentialsCreationStrategy, TSigningCredentialsCreationStrategy>();
        services.AddScoped<IJwtValidatorService, JwtValidatorService>();

        return services;
    }
}
