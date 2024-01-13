using Microsoft.AspNetCore.Http;

namespace Sample.Application.WebApi.Common.Accessors;
public class ContextAccessor(IHttpContextAccessor httpContextAccessor, IApplicationLifetimeAccessor applicationLifetimeAccessor) : IContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IApplicationLifetimeAccessor _applicationLifetimeAccessor = applicationLifetimeAccessor;

    public CancellationToken RequestCancellationToken => GetRequestCancellationToken();
    public CancellationToken CombinedCancellationToken => GetCombinedCancellationToken();

    private CancellationToken GetCombinedCancellationToken()
    {
        CancellationToken requestAbortedCancellationToken = GetRequestCancellationToken();
        CancellationToken applicationStoppingCancellationToken = _applicationLifetimeAccessor.ApplicationStoppingCancellationToken;
        CancellationToken applicationStoppedCancellationToken = _applicationLifetimeAccessor.ApplicationStoppedCancellationToken;

        CancellationTokenSource requestCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            requestAbortedCancellationToken,
            applicationStoppingCancellationToken,
            applicationStoppedCancellationToken
            );
        CancellationToken requestCancellationToken = requestCancellationTokenSource.Token;

        return requestCancellationToken;
    }

    private CancellationToken GetRequestCancellationToken()
    {
        HttpContext? httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null) throw new ArgumentNullException(nameof(httpContext));

        CancellationToken requestAbortedCancellationToken = httpContext.RequestAborted;
        return requestAbortedCancellationToken;
    }
}
