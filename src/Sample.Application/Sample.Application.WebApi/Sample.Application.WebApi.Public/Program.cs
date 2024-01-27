using Sample.Application.WebApi.Common.Constants;
using Sample.Application.WebApi.Common.Extensions.ServiceCollection;
using Sample.Application.WebApi.Common.Strategies;
using Sample.Architecture.Application.Extensions.ServiceCollection;
using Sample.Architecture.Extensions.Application.Authentication.Jwt.Options;
using Sample.Architecture.Extensions.Application.Mailing.Options;
using Sample.Architecture.Extensions.Infrastructure.Authentication.Common.Extensions.ServiceCollection;
using Sample.Architecture.Extensions.Infrastructure.Authentication.Jwt.Extensions.ServiceCollection;
using Sample.Architecture.Extensions.Infrastructure.Authentication.Jwt.Strategies;
using Sample.Architecture.Extensions.Infrastructure.Common.Extensions.ServiceCollection;
using Sample.Architecture.Extensions.Infrastructure.Mailing.Extensions.ServiceCollection;
using Sample.Architecture.Infrastructure.Extensions.ServiceCollection;
using System.Reflection;

Assembly callingAssembly = Assembly.GetCallingAssembly();

// Services from this assembly
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Services from Sample.Architecture.Extensions
builder.Services.AddJwtGenerator<JwtGeneratorSigningCredentialsCreationStrategy>();
builder.Services.AddJwtValidator<JwtValidatorSigningCredentialsCreationStrategy>();
builder.Services.AddAuthenticationUtilities();
builder.Services.AddMailSender();
builder.Services.AddCommonUtilities();

// Options
builder.Services.AddOptions<JwtValidatorOptions>().BindConfiguration(AppSettingsKeyConstants.JwtValidator);
builder.Services.AddOptions<JwtGeneratorOptions>().BindConfiguration(AppSettingsKeyConstants.JwtGenerator);
builder.Services.AddOptions<MailSenderOptions>().BindConfiguration(AppSettingsKeyConstants.MailSender);

// Services from Sample.Application.WebApi.Common
builder.Services.AddAccessors();
builder.Services.AddJwtAuthentication();

// Services from Sample.Architecture.Application
builder.Services.AddApplicationLayer();

// Services from Sample.Architecture.Infrastructure
builder.Services.AddInfrastructureLayer();

// Middlewares
WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
