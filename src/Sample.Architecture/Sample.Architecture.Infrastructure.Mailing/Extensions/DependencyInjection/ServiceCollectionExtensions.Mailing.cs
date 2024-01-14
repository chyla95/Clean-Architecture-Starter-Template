using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Application.Extensions.DependencyInjection;
using Sample.Architecture.Application.Mailing.Constants;
using Sample.Architecture.Application.Mailing.Options;
using Sample.Architecture.Application.Mailing.Utilities;
using Sample.Architecture.Infrastructure.Mailing.Factories;
using Sample.Architecture.Infrastructure.Mailing.Utilities;

namespace Sample.Architecture.Infrastructure.Mailing.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddMailing(this IServiceCollection services)
    {
        services.AddAndBindOptions<SmtpClientOptions>(AppSettingsKeyConstants.SmtpClient);

        services.AddTransient<ISmtpClient, SmtpClient>();
        services.AddTransient<SmtpClientFactory>();
        services.AddTransient<IMailSenderUtility, MailSenderUtility>();

        return services;
    }
}
