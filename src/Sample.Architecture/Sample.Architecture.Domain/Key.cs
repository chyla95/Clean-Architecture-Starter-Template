namespace Sample.Architecture.Domain;
public sealed record Key<T>(T Value) where T : struct
{
    public static implicit operator Key<T>(T value) => new(value);
    public static implicit operator T(Key<T> id) => id.Value;

    public override int GetHashCode() => Value.GetHashCode();
}

