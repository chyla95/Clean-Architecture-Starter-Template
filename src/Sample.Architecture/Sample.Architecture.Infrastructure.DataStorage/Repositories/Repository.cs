using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Infrastructure.DataStorage.Repositories;
internal abstract class Repository<TEntity, TId>(DataContext dataContext)
    where TId : struct
    where TEntity : Entity<TId>
{
    private readonly DbSet<TEntity> _dbSet = dataContext.Set<TEntity>();

    public DbSet<TEntity> DbSet => _dbSet;
    public IQueryable<TEntity> Queryable => _dbSet.AsQueryable();
}
