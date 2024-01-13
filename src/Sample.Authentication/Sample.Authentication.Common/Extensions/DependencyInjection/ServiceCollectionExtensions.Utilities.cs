using Microsoft.Extensions.DependencyInjection;
using Sample.Authentication.Common.Utilities;

namespace Sample.Authentication.Common.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationTools(this IServiceCollection services)
    {
        services.AddTransient<IRefreshTokenUtility, RefreshTokenUtility>();

        return services;
    }
}