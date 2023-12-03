using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sample.Api.Common.Utilities.SettingsFluentValidation;
public class FluentValidationOptions<TOptions>(string? name, IServiceProvider serviceProvider) : IValidateOptions<TOptions>
    where TOptions : class
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly string? _name = name;

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name is not null && _name != name) return ValidateOptionsResult.Skip;
        if (options is null) throw new ArgumentNullException(nameof(options));

        using IServiceScope scope = _serviceProvider.CreateScope();

        IValidator<TOptions> validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();
        ValidationResult results = validator.Validate(options);
        if (results.IsValid) return ValidateOptionsResult.Success;

        string typeName = options.GetType().Name;
        IEnumerable<string> errors = results.Errors
            .Select(vf => $"Validation for '{typeName}.{vf.PropertyName}' failed. Reason: '{vf.ErrorMessage}'.");

        return ValidateOptionsResult.Fail(errors);
    }
}
