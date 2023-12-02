using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Common.Accessors;

namespace Sample.Api.Common;

public static class ServiceCollections
{
    public static IServiceCollection AddAccessors(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationLifetimeAccessor, ApplicationLifetimeAccessor>();
        services.AddScoped<IContextAccessor, ContextAccessor>();
        services.AddScoped<ISettingsAccessor, SettingsAccessor>();

        return services;
    }
}
