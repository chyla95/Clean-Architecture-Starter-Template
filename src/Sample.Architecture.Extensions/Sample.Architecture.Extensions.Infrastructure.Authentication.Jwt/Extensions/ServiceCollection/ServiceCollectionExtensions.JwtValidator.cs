using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Strategies;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Utilities;
using Sample.Architecture.Extensions.Infrastructure.Authentication.Jwt.Utilities;

namespace Sample.Architecture.Extensions.Infrastructure.Authentication.Jwt.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtValidator<TSigningCredentialsCreationStrategy>(this IServiceCollection services)
        where TSigningCredentialsCreationStrategy : class, IJwtValidatorSigningCredentialsCreationStrategy
    {
        services.AddScoped<IJwtValidatorSigningCredentialsCreationStrategy, TSigningCredentialsCreationStrategy>();
        services.AddScoped<IJwtValidatorUtility, JwtValidatorUtility>();

        // Other
        services.AddTransient<IJwtClaimsUtility, JwtClaimsUtility>();

        return services;
    }
}
