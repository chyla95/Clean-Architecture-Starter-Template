using Microsoft.Extensions.DependencyInjection;
using Sample.Application.WebApi.Common.Accessors;

namespace Sample.Application.WebApi.Common.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccessors(this IServiceCollection services)
    {
        services.AddSingleton<IApplicationLifetimeAccessor, ApplicationLifetimeAccessor>();
        services.AddScoped<IContextAccessor, ContextAccessor>();

        return services;
    }
}
