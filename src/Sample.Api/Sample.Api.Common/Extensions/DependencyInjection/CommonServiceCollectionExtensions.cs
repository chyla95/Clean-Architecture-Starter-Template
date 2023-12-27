using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Common.Accessors;

namespace Sample.Api.Common.Extensions.DependencyInjection;
public static class CommonServiceCollectionExtensions
{
    public static IServiceCollection AddAccessors(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationLifetimeAccessor, ApplicationLifetimeAccessor>();
        services.AddScoped<IContextAccessor, ContextAccessor>();

        return services;
    }
}
