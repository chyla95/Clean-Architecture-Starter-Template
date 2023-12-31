using System.Reflection;
using Sample.Api.Authentication.Jwt.Extensions.DependencyInjection;
using Sample.Api.Authentication.Jwt.Strategies;
using Sample.Api.Common.Extensions.DependencyInjection;
using Sample.Api.Common.Strategies;
using Sample.Architecture.Application.Extensions.DependencyInjection;
using Sample.Architecture.Infrastructure.Extensions.DependencyInjection;

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

// Services from Sample.Api.Common
builder.Services.AddAccessors();

// Services from Sample.Api.Authentication.Jwt
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
