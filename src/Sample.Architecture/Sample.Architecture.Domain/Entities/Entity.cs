namespace Sample.Architecture.Domain.Entities;
public abstract class Entity<TId>
    where TId : struct
{
    public Key<TId>? Id { get; set; }
}
