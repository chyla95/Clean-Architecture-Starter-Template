using Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Application.DatabaseAccess.Repositories;
public interface IGenericRepository<TEntity>
    where TEntity : Entity
{
    IQueryBuilder<TEntity> QueryBuilder { get; }

    Task<TEntity?> GetSingleByIdAsync(int id, CancellationToken cancellationToken = default);
}
