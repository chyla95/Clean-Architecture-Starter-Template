namespace Sample.Api.Common.Accessors;

public interface IContextAccessor
{
	CancellationToken RequestCancellationToken { get; }
    CancellationToken CombinedCancellationToken { get; }
}