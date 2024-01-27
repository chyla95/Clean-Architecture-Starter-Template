using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Extensions.Application.Common.Abstractions.Utilities;
using Sample.Architecture.Extensions.Infrastructure.Common.Utilities;

namespace Sample.Architecture.Extensions.Infrastructure.Common.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonUtilities(this IServiceCollection services)
    {
        services.AddTransient<IFileUtility, FileUtility>();
        services.AddTransient<ITimeUtility, TimeUtility>();

        return services;
    }
}