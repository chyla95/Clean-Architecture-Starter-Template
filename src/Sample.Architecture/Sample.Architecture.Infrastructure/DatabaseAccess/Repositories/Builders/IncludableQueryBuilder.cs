using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sample.Architecture.Application.DatabaseAccess.Repositories.Builders;
using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;

namespace Sample.Architecture.Infrastructure.DatabaseAccess.Repositories.Builders;
internal class IncludableQueryBuilder<TEntity, TPreviouslyIncludedProperty>(IQueryable<TEntity> querable) : QueryBuilder<TEntity>(querable), IIncludableQueryBuilder<TEntity, TPreviouslyIncludedProperty>
    where TEntity : Entity
    where TPreviouslyIncludedProperty : Entity
{
    public IIncludableQueryBuilder<TEntity, TPropertyToInclude> ThenInclude<TPropertyToInclude>(Expression<Func<TPreviouslyIncludedProperty, TPropertyToInclude?>> include)
        where TPropertyToInclude : Entity
    {
        // Case when TPreviouslyIncludedProperty is taken from NOT a collection while ThenInclude'ing
        if (_querable.GetType().IsAssignableTo(typeof(IIncludableQueryable<TEntity, TPreviouslyIncludedProperty>)))
        {
            IIncludableQueryable<TEntity, TPreviouslyIncludedProperty> includableQueryable = (IIncludableQueryable<TEntity, TPreviouslyIncludedProperty>)_querable;
            _querable = includableQueryable.ThenInclude(include);
        }
        // Case when TPreviouslyIncludedProperty is taken from a collection while ThenInclude'ing
        else
        {
            IIncludableQueryable<TEntity, IEnumerable<TPreviouslyIncludedProperty>> includableQueryable = (IIncludableQueryable<TEntity, IEnumerable<TPreviouslyIncludedProperty>>)_querable;
            _querable = includableQueryable.ThenInclude(include);
        }
        return new IncludableQueryBuilder<TEntity, TPropertyToInclude>(_querable);
    }

    public IIncludableQueryBuilder<TEntity, TPropertyToInclude> ThenInclude<TPropertyToInclude>(Expression<Func<TPreviouslyIncludedProperty, IEnumerable<TPropertyToInclude>>> include)
        where TPropertyToInclude : Entity
    {
        // Case when TPreviouslyIncludedProperty is taken from NOT a collection while ThenInclude'ing
        if (_querable.GetType().IsAssignableTo(typeof(IIncludableQueryable<TEntity, TPreviouslyIncludedProperty>)))
        {
            IIncludableQueryable<TEntity, TPreviouslyIncludedProperty> includableQueryable = (IIncludableQueryable<TEntity, TPreviouslyIncludedProperty>)_querable;
            _querable = includableQueryable.ThenInclude(include);
        }
        // Case when TPreviouslyIncludedProperty is taken from a collection while ThenInclude'ing
        else
        {
            IIncludableQueryable<TEntity, IEnumerable<TPreviouslyIncludedProperty>> includableQueryable = (IIncludableQueryable<TEntity, IEnumerable<TPreviouslyIncludedProperty>>)_querable;
            _querable = includableQueryable.ThenInclude(include);
        }

        return new IncludableQueryBuilder<TEntity, TPropertyToInclude>(_querable);
    }
}
