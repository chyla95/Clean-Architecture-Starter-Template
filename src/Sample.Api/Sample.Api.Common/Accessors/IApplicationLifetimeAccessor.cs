namespace MC.Sample.Api.Utilities.Accessors;

public interface IApplicationLifetimeAccessor
{
	CancellationToken ApplicationStartedCancellationToken { get; }
	CancellationToken ApplicationStoppedCancellationToken { get; }
	CancellationToken ApplicationStoppingCancellationToken { get; }
}