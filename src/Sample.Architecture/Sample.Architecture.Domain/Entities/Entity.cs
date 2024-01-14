namespace Sample.Architecture.Domain.Entities;
public abstract class Entity<TId>
    where TId : struct
{
    public TId Id { get; set; }
}
