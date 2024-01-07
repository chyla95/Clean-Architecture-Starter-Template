using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;

namespace Sample.Architecture.Infrastructure.DatabaseAccess.Repositories.Builders;

internal class IncludableQueryBuilder<TEntity>(IQueryable<TEntity> querable) : QueryBuilder<TEntity>(querable), IIncludableQueryBuilder<TEntity>
    where TEntity : Entity
{
    public IThenIncludableQueryBuilder<TEntity, TPropertyToInclude> Include<TPropertyToInclude>(Expression<Func<TEntity, IEnumerable<TPropertyToInclude>>> include)
        where TPropertyToInclude : Entity
    {
        _querable = _querable.Include(include);
        return new ThenIncludableQueryBuilder<TEntity, TPropertyToInclude>(_querable);
    }

    public IThenIncludableQueryBuilder<TEntity, TPropertyToInclude> Include<TPropertyToInclude>(Expression<Func<TEntity, TPropertyToInclude?>> include)
        where TPropertyToInclude : Entity
    {
        _querable = _querable.Include(include);
        return new ThenIncludableQueryBuilder<TEntity, TPropertyToInclude>(_querable);
    }
}