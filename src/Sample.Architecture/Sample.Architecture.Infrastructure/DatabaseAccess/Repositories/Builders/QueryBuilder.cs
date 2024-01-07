using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;

namespace Sample.Architecture.Infrastructure.DatabaseAccess.Repositories.Builders;
internal sealed class QueryBuilder<TEntity>(IQueryable<TEntity> querable) : IQueryBuilder<TEntity>
    where TEntity : Entity
{
    private IQueryable<TEntity> _querable = querable;

    public IQueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> wherePredicate)
    {
        _querable = _querable.Where(wherePredicate);
        return this;
    }

    public IQueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> wherePredicate, bool shouldExecute)
    {
        if (!shouldExecute) return this;

        _querable = _querable.Where(wherePredicate);
        return this;
    }

    public IQueryBuilder<TEntity> Take(int count)
    {
        _querable = _querable.Take(count);
        return this;
    }

    public IQueryBuilder<TEntity> Skip(int count)
    {
        _querable = _querable.Skip(count);
        return this;
    }

    public IQueryBuilder<TEntity> OrderByAsc<TPropertyToOrderBy>(Expression<Func<TEntity, TPropertyToOrderBy>> orderBy)
    {
        _querable = _querable.OrderBy(orderBy);
        return this;
    }

    public IQueryBuilder<TEntity> OrderByDesc<TPropertyToOrderBy>(Expression<Func<TEntity, TPropertyToOrderBy>> orderBy)
    {
        _querable = _querable.OrderByDescending(orderBy);
        return this;
    }

    public Task<TEntity> GetSingleAsync(CancellationToken cancellationToken = default)
    {
        Task<TEntity> entity = _querable.SingleAsync(cancellationToken);
        return entity;
    }

    public Task<TEntity?> GetSingleOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        Task<TEntity?> entity = _querable.SingleOrDefaultAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetManyAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entities = await _querable.ToListAsync(cancellationToken);
        return entities;
    }

    public async Task<IEnumerable<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1) throw new ArgumentException("Page number must be greater than 0.");
        if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0.");

        int numberOfEntitiesToSkip = (pageNumber - 1) * pageSize;

        IEnumerable<TEntity> entities = await _querable
            .Skip(numberOfEntitiesToSkip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return entities;
    }

    public async Task<int> GetPageCountAsync(int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0.");

        int numberOfEntities = await _querable.CountAsync(cancellationToken);
        int numberOfPages = numberOfEntities / pageSize;

        return numberOfPages;
    }
}
