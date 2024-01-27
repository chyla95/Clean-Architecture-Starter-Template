using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Utilities;

namespace Sample.Application.WebApi.Common.Extensions.ServiceCollection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IJwtValidatorUtility jwtValidatorService = serviceProvider.GetRequiredService<IJwtValidatorUtility>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = async (messageReceivedContext) =>
                    {
                        TokenValidationParameters tokenValidationParameters = await jwtValidatorService.GenerateTokenValidationParametersAsync();
                        messageReceivedContext.Options.TokenValidationParameters = tokenValidationParameters;
                    }
                };
            });

        return services;
    }
}
