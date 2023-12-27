using Microsoft.Extensions.DependencyInjection;

namespace Sample.Architecture.Infrastructure.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        return services;
    }
}
