using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Api.Common.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptions<TOptions>(this IServiceCollection services, string sectionPath, Action<BinderOptions>? configureBinder = null)
        where TOptions : class, new()
    {
        services
            .AddOptions<TOptions>()
            .BindConfiguration(sectionPath, configureBinder);

        return services;
    }

    public static IServiceCollection AddAndValidateOptions<TOptions>(this IServiceCollection services, string sectionPath, Action<BinderOptions>? configureBinder = null)
        where TOptions : class, new()
    {
        throw new NotImplementedException();
    }
}
