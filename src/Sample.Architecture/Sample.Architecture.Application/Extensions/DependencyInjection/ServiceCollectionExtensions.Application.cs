using Microsoft.Extensions.DependencyInjection;

namespace Sample.Architecture.Application.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddUtilities();

        return services;
    }
}
