using Sample.Application.WebApi.Common.Extensions.DependencyInjection;
using Sample.Application.WebApi.Common.Strategies;
using Sample.Architecture.Application.Extensions.DependencyInjection;
using Sample.Architecture.Infrastructure.Extensions.DependencyInjection;
using Sample.Architecture.Infrastructure.Mailing.Extensions.DependencyInjection;
using Sample.Authentication.Common.Extensions.DependencyInjection;
using Sample.Authentication.Jwt.Extensions.DependencyInjection;
using Sample.Authentication.Jwt.Strategies;
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

// Services from Sample.Architecture.Application
builder.Services.AddApplicationLayer();

// Services from Sample.Architecture.Infrastructure
builder.Services.AddInfrastructureLayer();

// Services from Sample.Architecture.Infrastructure.Mailing
builder.Services.AddMailSender();

// Services from Sample.Application.WebApi.Common
builder.Services.AddAccessors();

// Services from Sample.Authentication.Common
builder.Services.AddAuthenticationTools();

// Services from Sample.Authentication.Jwt
builder.Services.AddJwtGenerator<JwtGeneratorSigningCredentialsCreationStrategy>();
builder.Services.AddJwtValidator<JwtValidatorSigningCredentialsCreationStrategy>();
builder.Services.AddJwtAuthentication();

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
