using Microsoft.Extensions.DependencyInjection;
using Sample.Architecture.Extensions.Application.Mailing.Factories;
using Sample.Architecture.Extensions.Application.Mailing.Utilities;
using Sample.Architecture.Extensions.Infrastructure.Mailing.Factories;
using Sample.Architecture.Extensions.Infrastructure.Mailing.Utilities;

namespace Sample.Architecture.Extensions.Infrastructure.Mailing.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddMailSender(this IServiceCollection services)
    {
        services.AddScoped<IMailSenderClientFactory, MailSenderClientFactory>();
        services.AddScoped<IMailSenderFactory, MailSenderFactory>();
        services.AddScoped<IMailSenderUtility, MailSenderUtility>();

        return services;
    }
}
