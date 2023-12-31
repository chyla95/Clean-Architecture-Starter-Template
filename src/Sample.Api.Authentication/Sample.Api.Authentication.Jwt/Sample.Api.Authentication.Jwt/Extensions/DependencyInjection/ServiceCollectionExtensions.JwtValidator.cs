using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Authentication.Jwt.Options;
using Sample.Api.Authentication.Jwt.Services;
using Sample.Api.Authentication.Jwt.Strategies;

namespace Sample.Api.Authentication.Jwt.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtValidator<TSigningCredentialsCreationStrategy>(this IServiceCollection services, string jwtValidatorOptionsSectionName)
        where TSigningCredentialsCreationStrategy : class, IJwtValidatorSigningCredentialsCreationStrategy
    {
        services.AddOptions<JwtValidatorOptions>().BindConfiguration(jwtValidatorOptionsSectionName);
        services.AddScoped<IJwtValidatorSigningCredentialsCreationStrategy, TSigningCredentialsCreationStrategy>();
        services.AddScoped<IJwtValidatorService, JwtValidatorService>();

        return services;
    }
}
