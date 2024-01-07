using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Application.DatabaseAccess.Repositories;
public interface IGenericRepository<TEntity> 
    where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetManyAsync();
}
