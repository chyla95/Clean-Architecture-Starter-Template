using Microsoft.EntityFrameworkCore;
using Sample.Architecture.Application.DataStorage.Models;
using Sample.Architecture.Application.DataStorage.Repositories;
using Sample.Architecture.Domain.Entities;
using System.Linq.Expressions;

namespace Sample.Architecture.Infrastructure.DataStorage.Repositories;
internal abstract class GenericRepository<TEntity, TId>(DataContext dataContext) : Repository<TEntity, TId>(dataContext), IGenericRepository<TEntity, TId>
    where TId : struct
    where TEntity : Entity<TId>
{
    public Task<TEntity?> GetSingleAsync(TId id, CancellationToken cancellationToken = default)
    {
        return Queryable.SingleOrDefaultAsync(e => e.Id.Equals(id), cancellationToken);
    }

    public Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return Queryable.SingleOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetManyAsync(CancellationToken cancellationToken = default)
    {
        return await Queryable.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetManyAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default)
    {
        return await Queryable.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await Queryable.Where(filter).ToListAsync(cancellationToken);
    }

    public async Task<Page<TEntity, TId>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1) throw new ArgumentException("Page number must be greater than 0.", nameof(pageNumber));
        if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0.", nameof(pageSize));

        int countOfEntitiesToSkip = (pageNumber - 1) * pageSize;

        int totalEntityCount = await Queryable.CountAsync(cancellationToken);
        List<TEntity> pagedEntities = await Queryable
            .Skip(countOfEntitiesToSkip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        int totalPagesCount = (int)Math.Ceiling(totalEntityCount / (double)pageSize);
        bool hasPreviousPage = pageNumber > 1;
        bool hasNextPage = pageNumber < totalPagesCount;

        return new Page<TEntity, TId>(pagedEntities, pageNumber, totalPagesCount, hasPreviousPage, hasNextPage);
    }

    public async Task<Page<TEntity, TId>> GetPageAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1) throw new ArgumentException("Page number must be greater than 0.", nameof(pageNumber));
        if (pageSize < 1) throw new ArgumentException("Page size must be greater than 0.", nameof(pageSize));

        int countOfEntitiesToSkip = (pageNumber - 1) * pageSize;

        int totalEntityCount = await Queryable.CountAsync(filter, cancellationToken);
        List<TEntity> pagedEntities = await Queryable
            .Where(filter)
            .Skip(countOfEntitiesToSkip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        int totalPagesCount = (int)Math.Ceiling(totalEntityCount / (double)pageSize);
        bool hasPreviousPage = pageNumber > 1;
        bool hasNextPage = pageNumber < totalPagesCount;

        return new Page<TEntity, TId>(pagedEntities, pageNumber, totalPagesCount, hasPreviousPage, hasNextPage);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return Queryable.AnyAsync(filter, cancellationToken);
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
    {
        return Queryable.CountAsync(filter, cancellationToken);
    }

    public void Add(TEntity entity) => DbSet.Add(entity);

    public void Add(IEnumerable<TEntity> entities) => DbSet.AddRange(entities);

    public void Remove(TEntity entity) => DbSet.Remove(entity);

    public void Remove(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);

    public void UpdateAsync(TEntity entity) => DbSet.Update(entity);

    public void UpdateAsync(IEnumerable<TEntity> entities) => DbSet.UpdateRange(entities);
}
