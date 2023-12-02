﻿namespace Sample.Api.Common.Accessors;

public interface IApplicationLifetimeAccessor
{
	CancellationToken ApplicationStartedCancellationToken { get; }
	CancellationToken ApplicationStoppedCancellationToken { get; }
	CancellationToken ApplicationStoppingCancellationToken { get; }
}