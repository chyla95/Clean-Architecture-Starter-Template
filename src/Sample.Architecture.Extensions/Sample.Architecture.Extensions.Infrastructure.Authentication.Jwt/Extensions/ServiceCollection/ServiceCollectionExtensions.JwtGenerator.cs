using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Strategies;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Utilities;
using Sample.Architecture.Extensions.Infrastructure.Authentication.Jwt.Utilities;

namespace Sample.Architecture.Extensions.Infrastructure.Authentication.Jwt.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtGenerator<TSigningCredentialsCreationStrategy>(this IServiceCollection services)
        where TSigningCredentialsCreationStrategy : class, IJwtGeneratorSigningCredentialsCreationStrategy
    {
        services.AddScoped<IJwtGeneratorSigningCredentialsCreationStrategy, TSigningCredentialsCreationStrategy>();
        services.AddScoped<IJwtGeneratorUtility, JwtGeneratorUtility>();

        return services;
    }
}
