using Aidelythe.Api._System.Authentication.Services;
using Aidelythe.Shared.Guards;

namespace Aidelythe.Api._System.Telemetry.Logging;

/// <summary>
/// Represents middleware for enriching the logging context with request-specific properties.
/// </summary>
public sealed class RequestLogContextMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequestLogContextMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next delegate in the HTTP request pipeline.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="next"/> is null.</exception>
    public RequestLogContextMiddleware(RequestDelegate next)
    {
        ThrowIfNull(next);

        _next = next;
    }

    /// <summary>
    /// Enriches the logging context with request-specific properties.
    /// </summary>
    /// <param name="httpContext">The HTTP context associated with the current request.</param>
    /// <param name="userSessionContextAccessor">The instance of <see cref="IUserSessionContextAccessor"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="httpContext"/> or <paramref name="userSessionContextAccessor"/> is null.</exception>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    public async Task InvokeAsync(
        HttpContext httpContext,
        IUserSessionContextAccessor userSessionContextAccessor)
    {
        ThrowIfNull(httpContext);
        ThrowIfNull(userSessionContextAccessor);

        var clientIp = httpContext.Connection.RemoteIpAddress.ThrowIfNull();
        var userId = userSessionContextAccessor.UserSessionContext?.UserId;
        var userSessionId = userSessionContextAccessor.UserSessionContext?.UserSessionId;

        using (LogContext.PushProperty("ClientIp", $"{clientIp}", destructureObjects: false))
        using (LogContext.PushProperty("UserId", $"{userId}", destructureObjects: false))
        using (LogContext.PushProperty("UserSessionId", $"{userSessionId}", destructureObjects: false))
        {
            await _next(httpContext);
        }
    }
}