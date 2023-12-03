using FluentValidation;
using Sample.Api.Common;
using Sample.Api.Public.Settings;
using Sample.Api.Public.Validators.Settings;
using System.Reflection;
using Sample.Api.Common.Extensions;

Assembly callingAssembly = Assembly.GetCallingAssembly();

// Services
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAccessors();

builder.Services.AddValidatorsFromAssembly(callingAssembly);
builder.Services.ValidateSettings<JwtSettings, JwtSettingsValidator>(JwtSettings.SettingsSectionName);

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
