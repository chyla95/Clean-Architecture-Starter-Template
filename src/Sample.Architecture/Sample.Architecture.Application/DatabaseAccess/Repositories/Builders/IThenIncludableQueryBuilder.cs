using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;

namespace Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
public interface IThenIncludableQueryBuilder<TEntity, TPreviouslyIncludedProperty> : IIncludableQueryBuilder<TEntity>
    where TEntity : Entity
    where TPreviouslyIncludedProperty : Entity
{
    IThenIncludableQueryBuilder<TEntity, TPropertyToInclude> ThenInclude<TPropertyToInclude>(Expression<Func<TPreviouslyIncludedProperty, TPropertyToInclude?>> include)
        where TPropertyToInclude : Entity;

    IThenIncludableQueryBuilder<TEntity, TPropertyToInclude> ThenInclude<TPropertyToInclude>(Expression<Func<TPreviouslyIncludedProperty, IEnumerable<TPropertyToInclude>>> include)
        where TPropertyToInclude : Entity;
}
