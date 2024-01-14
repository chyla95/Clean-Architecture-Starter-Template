using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sample.Architecture.Application.DataStorage;
using Sample.Architecture.Infrastructure.DataStorage.Options;
using Sample.Architecture.Application.Extensions.DependencyInjection;
using Sample.Architecture.Infrastructure.DataStorage.Constants;

namespace Sample.Architecture.Infrastructure.DataStorage.Extensions.DependencyInjection;
public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataStorage(this IServiceCollection services)
    {
        services.AddAndBindOptions<DatabaseAccessOptions>(AppSettingsKeyConstants.DatabaseAccess);

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        IOptions<DatabaseAccessOptions> databaseAccessOptions = serviceProvider.GetRequiredService<IOptions<DatabaseAccessOptions>>();

        services.AddDbContext<DataContext>(dbContextOptionsBuilder => dbContextOptionsBuilder.UseNpgsql(databaseAccessOptions.Value.ConnectionString));
        services.AddTransient<IDataUnitOfWork, DataUnitOfWork>();

        return services;
    }
}