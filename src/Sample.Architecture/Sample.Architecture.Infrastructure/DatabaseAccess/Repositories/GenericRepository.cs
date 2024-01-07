using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Application.DatabaseAccess.Repositories;
using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Infrastructure.DatabaseAccess.Repositories;
internal abstract class GenericRepository<TEntity>(DataContext dataContext) : Repository<TEntity>(dataContext), IGenericRepository<TEntity>
    where TEntity : Entity
{
    public async Task<IEnumerable<TEntity>> GetManyAsync()
    {
        IQueryable<TEntity> queryable = BuildGetManyQuerable();
        return await queryable.ToListAsync();
    }
}
