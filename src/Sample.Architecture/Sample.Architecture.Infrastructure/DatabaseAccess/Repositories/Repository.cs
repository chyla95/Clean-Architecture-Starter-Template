using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Infrastructure.DatabaseAccess.Repositories;
internal abstract class Repository<TEntity>(DataContext dataContext)
    where TEntity : Entity
{
    private readonly DbSet<TEntity> _dbSet = dataContext.Set<TEntity>();

    public DbSet<TEntity> DbSet => _dbSet;
    public IQueryable<TEntity> Queryable => _dbSet.AsQueryable();
}
