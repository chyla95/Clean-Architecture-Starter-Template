using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sample.Architecture.Domain.Entities;
using Sample.Architecture.Infrastructure.Options;
using System.Reflection;

namespace Sample.Architecture.Infrastructure.DatabaseAccess;
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