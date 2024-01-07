namespace Sample.Architecture.Infrastructure.Options;
public sealed record DatabaseAccessOptions
{
    public required string ConnectionString { get; init; }
    public required string SchemaName { get; init; }
}