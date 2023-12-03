using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sample.Api.Common.Extensions;
public static class ServiceCollectionExtensions
{
    public static OptionsBuilder<TOptions> ValidateSettings<TOptions, TValidator>(this IServiceCollection services, string configurationSection)
    where TOptions : class
    where TValidator : class, IValidator<TOptions>
    {
        services.AddScoped<IValidator<TOptions>, TValidator>();

        return services.AddOptions<TOptions>()
            .BindConfiguration(configurationSection)
            .ValidateWithFluentValidation()
            .ValidateOnStart();
    }
}
