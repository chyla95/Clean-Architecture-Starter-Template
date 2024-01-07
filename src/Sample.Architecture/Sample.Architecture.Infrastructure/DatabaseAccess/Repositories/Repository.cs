using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Infrastructure.DatabaseAccess.Repositories;
internal abstract class Repository<TEntity>(DataContext dataContext)
    where TEntity : Entity
{
    private readonly DataContext _dataContext = dataContext;
    private readonly DbSet<TEntity> _dbSet = dataContext.Set<TEntity>();

    public DbSet<TEntity> DbSet => _dbSet;

    protected virtual IQueryable<TEntity> BuildGetSingleQuerable()
        => BuildQuerable();

    protected virtual IQueryable<TEntity> BuildGetManyQuerable()
        => BuildQuerable();

    private IQueryable<TEntity> BuildQuerable()
        => _dataContext.Set<TEntity>().AsQueryable();
}
