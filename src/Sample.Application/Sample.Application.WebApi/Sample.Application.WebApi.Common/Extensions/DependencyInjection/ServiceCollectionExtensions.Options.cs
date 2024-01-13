using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Application.WebApi.Common.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptionsWithBindings<TOptions>(this IServiceCollection services, string sectionPath, Action<BinderOptions>? configureBinder = null)
        where TOptions : class
    {
        services
            .AddOptions<TOptions>()
            .BindConfiguration(sectionPath, configureBinder);

        return services;
    }
}
