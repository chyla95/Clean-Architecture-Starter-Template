using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Application.DatabaseAccess.Repositories;
using Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
using Sample.Architecture.Domain.Entities;
using Sample.Architecture.Infrastructure.DatabaseAccess.Repositories.Builders;

namespace Sample.Architecture.Infrastructure.DatabaseAccess.Repositories;
internal abstract class GenericRepository<TEntity>(DataContext dataContext) : Repository<TEntity>(dataContext), IGenericRepository<TEntity>
    where TEntity : Entity
{
    public IIncludableQueryBuilder<TEntity> QueryBuilder => new IncludableQueryBuilder<TEntity>(Queryable);

    public virtual Task<TEntity?> GetSingleByIdAsync(int id, CancellationToken cancellationToken = default) 
        => Queryable.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
}
