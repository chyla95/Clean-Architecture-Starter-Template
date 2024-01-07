using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sample.Api.Common.Extensions.DependencyInjection;
using Sample.Architecture.Application.DatabaseAccess;
using Sample.Architecture.Infrastructure.Constants;
using Sample.Architecture.Infrastructure.DatabaseAccess;
using Sample.Architecture.Infrastructure.Options;

namespace Sample.Architecture.Infrastructure.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddAndBindOptions<DatabaseAccessOptions>(AppSettingsKeyConstants.DatabaseAccess);

        ServiceProvider serviceProvider = services.BuildServiceProvider();        
        IOptions<DatabaseAccessOptions> databaseAccessOptions = serviceProvider.GetRequiredService<IOptions<DatabaseAccessOptions>>();

        services.AddDbContext<DataContext>(dbContextOptionsBuilder => dbContextOptionsBuilder.UseNpgsql(databaseAccessOptions.Value.ConnectionString));
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
