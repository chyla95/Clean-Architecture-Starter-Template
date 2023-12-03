using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sample.Api.Common.Utilities.SettingsFluentValidation;

namespace Sample.Api.Common.Extensions;
public static class OptionsBuilderExtensions
{
    public static OptionsBuilder<TOptions> ValidateWithFluentValidation<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
        where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(
            provider => new FluentValidationOptions<TOptions>(optionsBuilder.Name, provider)
        );

        return optionsBuilder;
    }
}
