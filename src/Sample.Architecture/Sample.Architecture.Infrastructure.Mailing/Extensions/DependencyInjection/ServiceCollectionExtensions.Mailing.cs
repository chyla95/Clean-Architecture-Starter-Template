using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Application.Extensions.DependencyInjection;
using Sample.Architecture.Application.Mailing.Constants;
using Sample.Architecture.Application.Mailing.Factories;
using Sample.Architecture.Application.Mailing.Options;
using Sample.Architecture.Application.Mailing.Utilities;
using Sample.Architecture.Infrastructure.Mailing.Factories;
using Sample.Architecture.Infrastructure.Mailing.Utilities;

namespace Sample.Architecture.Infrastructure.Mailing.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddMailing(this IServiceCollection services)
    {
        services.AddAndBindOptions<MailingSenderOptions>(AppSettingsKeyConstants.SmtpClient);

        services.AddScoped<SmtpClientFactory>();
        services.AddScoped<IMailSenderFactory, MailSenderFactory>();
        services.AddScoped<IMailSenderUtility, MailSenderUtility>();

        return services;
    }
}
