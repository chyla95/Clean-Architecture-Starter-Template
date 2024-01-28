using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Extensions.Application.Authentication.Common.Utilities;
using Sample.Architecture.Extensions.Infrastructure.Authentication.Common.Utilities;

namespace Sample.Architecture.Extensions.Infrastructure.Authentication.Common.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationUtilities(this IServiceCollection services)
    {
        services.AddTransient<IRefreshTokenUtility, RefreshTokenUtility>();

        return services;
    }
}