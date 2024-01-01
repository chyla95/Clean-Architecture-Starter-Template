using Microsoft.Extensions.Hosting;

namespace Sample.Api.Common.Accessors;
public class ApplicationLifetimeAccessor(IHostApplicationLifetime hostApplicationLifetime) : IApplicationLifetimeAccessor
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime = hostApplicationLifetime;

    public CancellationToken ApplicationStartedCancellationToken => _hostApplicationLifetime.ApplicationStarted;
    public CancellationToken ApplicationStoppingCancellationToken => _hostApplicationLifetime.ApplicationStopping;
    public CancellationToken ApplicationStoppedCancellationToken => _hostApplicationLifetime.ApplicationStopped;
}
