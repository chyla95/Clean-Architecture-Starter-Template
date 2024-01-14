using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Domain.Entities;
using Sample.Architecture.Infrastructure.DataStorage.Options;

namespace Sample.Architecture.Infrastructure.DataStorage;
internal sealed class DataContext(DbContextOptions<DataContext> dbContextOptions, IOptions<DatabaseAccessOptions> databaseAccessOptions) : DbContext(dbContextOptions)
{
    private readonly IOptions<DatabaseAccessOptions> _databaseAccessOptions = databaseAccessOptions;

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(executingAssembly);

        modelBuilder.HasDefaultSchema(_databaseAccessOptions.Value.SchemaName);
    }
}

// cd .\src\Sample.Architecture\Sample.Architecture.Infrastructure.DataStorage\
// dotnet ef migrations add AddUserModel -o .\Migrations\ -s ..\..\Sample.Application\Sample.Application.ConsoleApp\Sample.Application.ConsoleApp.DeploymentTools\
// dotnet ef database update -s ..\..\Sample.Application\Sample.Application.ConsoleApp\Sample.Application.ConsoleApp.DeploymentTools\