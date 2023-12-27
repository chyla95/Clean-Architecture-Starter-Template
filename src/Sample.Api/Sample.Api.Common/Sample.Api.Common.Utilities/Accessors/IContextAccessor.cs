namespace Sample.Api.Common.Utilities.Accessors;
public interface IContextAccessor
{
	CancellationToken RequestCancellationToken { get; }
    CancellationToken CombinedCancellationToken { get; }
}