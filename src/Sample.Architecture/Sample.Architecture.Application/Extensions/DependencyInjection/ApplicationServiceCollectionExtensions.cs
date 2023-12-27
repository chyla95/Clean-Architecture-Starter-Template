using Microsoft.Extensions.DependencyInjection;

namespace Sample.Architecture.Application.Extensions.DependencyInjection;
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        return services;
    }
}
