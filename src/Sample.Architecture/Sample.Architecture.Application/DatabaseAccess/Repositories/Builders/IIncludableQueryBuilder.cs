using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;

namespace Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
public interface IIncludableQueryBuilder<TEntity, TPreviouslyIncludedProperty> : IQueryBuilder<TEntity>
    where TEntity : Entity
    where TPreviouslyIncludedProperty : Entity
{
    IIncludableQueryBuilder<TEntity, TPropertyToInclude> ThenInclude<TPropertyToInclude>(Expression<Func<TPreviouslyIncludedProperty, TPropertyToInclude?>> include)
        where TPropertyToInclude : Entity;

    IIncludableQueryBuilder<TEntity, TPropertyToInclude> ThenInclude<TPropertyToInclude>(Expression<Func<TPreviouslyIncludedProperty, IEnumerable<TPropertyToInclude>>> include)
        where TPropertyToInclude : Entity;
}
