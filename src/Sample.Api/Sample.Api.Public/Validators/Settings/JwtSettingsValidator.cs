using FluentValidation;
using Sample.Api.Public.Settings;

namespace Sample.Api.Public.Validators.Settings;

internal class JwtSettingsValidator : AbstractValidator<JwtSettings>
{
    public JwtSettingsValidator()
    {
        RuleFor(js => js.Secret).NotEmpty().MinimumLength(100);
    }
}
