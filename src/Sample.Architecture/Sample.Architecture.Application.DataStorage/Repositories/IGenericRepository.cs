using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;
using Sample.Architecture.Application.DataStorage.Models;
namespace Sample.Architecture.Application.DataStorage.Repositories;
public interface IGenericRepository<TEntity, TId>
    where TId : struct
    where TEntity : Entity<TId>
{
    // Queries
    Task<TEntity?> GetSingleAsync(TId id, CancellationToken cancellationToken = default);
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetManyAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetManyAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

    Task<Page<TEntity, TId>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<Page<TEntity, TId>> GetPageAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default);

    // Commands
    void Add(TEntity entity);
    void Add(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void Remove(IEnumerable<TEntity> entities);
    void UpdateAsync(TEntity entity);
    void UpdateAsync(IEnumerable<TEntity> entities);
}
