namespace Sample.Application.WebApi.Common.Accessors;
public interface IContextAccessor
{
	CancellationToken RequestCancellationToken { get; }
    CancellationToken CombinedCancellationToken { get; }
}