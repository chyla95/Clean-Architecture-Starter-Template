using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Application.Utilities;

namespace Sample.Architecture.Application.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    private static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddTransient<IFileUtility, FileUtility>();
        services.AddTransient<ITimeUtility, TimeUtility>();

        return services;
    }
}
