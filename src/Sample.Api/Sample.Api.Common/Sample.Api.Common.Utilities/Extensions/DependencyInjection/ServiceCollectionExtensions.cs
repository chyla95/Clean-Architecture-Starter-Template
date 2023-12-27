using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Common.Utilities.Accessors;

namespace Sample.Api.Common.Utilities.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccessors(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationLifetimeAccessor, ApplicationLifetimeAccessor>();
        services.AddScoped<IContextAccessor, ContextAccessor>();

        return services;
    }
}
