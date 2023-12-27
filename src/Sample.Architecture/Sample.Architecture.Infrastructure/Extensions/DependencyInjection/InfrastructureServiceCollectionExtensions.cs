using Microsoft.Extensions.DependencyInjection;

namespace Sample.Architecture.Infrastructure.Extensions.DependencyInjection;
public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        return services;
    }
}
