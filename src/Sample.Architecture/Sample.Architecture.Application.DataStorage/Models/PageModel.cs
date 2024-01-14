using Sample.Architecture.Domain.Entities;

namespace Sample.Architecture.Application.DataStorage.Models;
public sealed record PageModel<TEntity, TId> (IEnumerable<TEntity> Entities , int CurrentPageNumber, int TotalPagesCount, bool HasPreviousPage, bool HasNextPage)
    where TId : struct
    where TEntity : Entity<TId>;
