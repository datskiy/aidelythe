using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Api._System.Authentication.Services;
using Aidelythe.Shared.Guards;
using Aidelythe.Shared.Tasks;

namespace Aidelythe.Api._System.Bandwidth;

/// <summary>
/// Provides extension methods for configuring bandwidth services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds rate limiting services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>
    /// The <see cref="IServiceCollection"/> with rate limiting services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="services"/> is null.</exception>
    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        ThrowIfNull(services);

        return services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = OverrideRejectionResponse();
            options.GlobalLimiter = CreateFixedWindowPartitionedRateLimiter();
        });
    }

    private static Func<OnRejectedContext, CancellationToken, ValueTask> OverrideRejectionResponse()
    {
        return (context, cancellationToken) =>
        {
            var httpContext = context.HttpContext;

            if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                var retryAfterSeconds = Math.Max((int)Math.Ceiling(retryAfter.TotalSeconds), 1);
                httpContext.Response.Headers.RetryAfter = $"{retryAfterSeconds}"; // TODO: check the bug after .NET upd
            }

            return httpContext.Response
                .WriteAsJsonAsync(
                    new TooManyRequestsResponse(httpContext.TraceIdentifier),
                    cancellationToken)
                .ToValueTask();
        };
    }

    private static PartitionedRateLimiter<HttpContext> CreateFixedWindowPartitionedRateLimiter()
    {
        // TODO: get from config as options
        var anonymousPermitLimit = 10;
        var authenticatedPermitLimit = 30;
        var windowSize = TimeSpan.FromSeconds(60);

        return PartitionedRateLimiter.Create<HttpContext, RateLimiterPartitionKey>(httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: CreateRateLimiterPartitionKey(httpContext),
                factory: partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    Window = windowSize,
                    QueueLimit = 0,
                    PermitLimit = partition.IsAuthenticated
                        ? authenticatedPermitLimit
                        : anonymousPermitLimit
                }));
    }

    private static RateLimiterPartitionKey CreateRateLimiterPartitionKey(HttpContext httpContext)
    {
        var userSessionContextAccessor = httpContext.RequestServices
            .GetRequiredService<IUserSessionContextAccessor>();

        return userSessionContextAccessor.UserSessionContext is null
            ? new RateLimiterPartitionKey(
                Value: $"{httpContext.Connection.RemoteIpAddress.ThrowIfNull()}",
                IsAuthenticated: false)
            : new RateLimiterPartitionKey(
                Value: $"{userSessionContextAccessor.UserSessionContext.UserId}",
                IsAuthenticated: true);
    }
}