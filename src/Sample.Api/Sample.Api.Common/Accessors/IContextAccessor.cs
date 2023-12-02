namespace MC.Sample.Api.Utilities.Accessors;

public interface IContextAccessor
{
	CancellationToken RequestCancellationToken { get; }
    CancellationToken CombinedCancellationToken { get; }
}