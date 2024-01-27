using Microsoft.Extensions.DependencyInjection;

namespace Sample.Architecture.Application.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}
