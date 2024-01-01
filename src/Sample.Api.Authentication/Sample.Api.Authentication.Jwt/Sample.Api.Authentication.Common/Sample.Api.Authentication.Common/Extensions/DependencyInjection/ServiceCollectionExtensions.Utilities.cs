using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Authentication.Common.Utilities;

namespace Sample.Api.Authentication.Common.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationTools(this IServiceCollection services)
    {
        services.AddTransient<IRefreshTokenUtility, RefreshTokenUtility>();

        return services;
    }
}