namespace Sample.Api.Common.Utilities.Accessors;
public interface IApplicationLifetimeAccessor
{
	CancellationToken ApplicationStartedCancellationToken { get; }
	CancellationToken ApplicationStoppedCancellationToken { get; }
	CancellationToken ApplicationStoppingCancellationToken { get; }
}