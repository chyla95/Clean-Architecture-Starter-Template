using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;

namespace Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
public interface IIncludableQueryBuilder<TEntity> : IQueryBuilder<TEntity>
    where TEntity : Entity
{
    IThenIncludableQueryBuilder<TEntity, TPropertyToInclude> Include<TPropertyToInclude>(Expression<Func<TEntity, IEnumerable<TPropertyToInclude>>> include)
        where TPropertyToInclude : Entity;

    public IThenIncludableQueryBuilder<TEntity, TPropertyToInclude> Include<TPropertyToInclude>(Expression<Func<TEntity, TPropertyToInclude?>> include)
        where TPropertyToInclude : Entity;
}
