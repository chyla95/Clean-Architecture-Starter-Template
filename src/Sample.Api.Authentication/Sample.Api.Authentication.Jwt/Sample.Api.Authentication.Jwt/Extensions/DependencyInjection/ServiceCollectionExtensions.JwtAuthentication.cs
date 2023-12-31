using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Authentication.Jwt.Services;

namespace Sample.Api.Authentication.Jwt.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IJwtValidatorService jwtValidatorSigningCredentialsCreationStrategy = serviceProvider.GetRequiredService<IJwtValidatorService>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = async (messageReceivedContext) =>
                    {
                        messageReceivedContext.Options.TokenValidationParameters = await jwtValidatorSigningCredentialsCreationStrategy.GenerateTokenValidationParametersAsync();
                    }
                };
            });


        return services;
    }
}
