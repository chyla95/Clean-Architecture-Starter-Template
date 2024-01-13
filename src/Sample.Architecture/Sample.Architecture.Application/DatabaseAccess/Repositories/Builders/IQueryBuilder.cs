using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;

namespace Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
public interface IQueryBuilder<TEntity>
    where TEntity : Entity
{
    IQueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> wherePredicate);
    IQueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> wherePredicate, bool shouldExecute);
    IQueryBuilder<TEntity> Take(int count);
    IQueryBuilder<TEntity> Skip(int count);
    IQueryBuilder<TEntity> OrderByAsc<TPropertyToOrderBy>(Expression<Func<TEntity, TPropertyToOrderBy>> orderBy);
    IQueryBuilder<TEntity> OrderByDesc<TPropertyToOrderBy>(Expression<Func<TEntity, TPropertyToOrderBy>> orderBy);

    IIncludableQueryBuilder<TEntity, TPropertyToInclude> Include<TPropertyToInclude>(Expression<Func<TEntity, IEnumerable<TPropertyToInclude>>> include)
        where TPropertyToInclude : Entity;

    public IIncludableQueryBuilder<TEntity, TPropertyToInclude> Include<TPropertyToInclude>(Expression<Func<TEntity, TPropertyToInclude?>> include)
        where TPropertyToInclude : Entity;

    Task<TEntity> GetSingleAsync(CancellationToken cancellationToken = default);
    Task<TEntity?> GetSingleOrDefaultAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetManyAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<int> GetPageCountAsync(int pageSize, CancellationToken cancellationToken = default);
}
