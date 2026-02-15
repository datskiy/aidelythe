using Aidelythe.Api._Common.Configuration;
using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Api._System.Authentication.Services;
using Aidelythe.Infrastructure._Common.Settings;
using Aidelythe.Shared.Guards;
using Aidelythe.Shared.Tasks;

namespace Aidelythe.Api._System.Bandwidth;

/// <summary>
/// Provides extension methods for configuring bandwidth services in the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds rate-limiting services to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>
    /// The service collection with rate-limiting services added.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="services"/> or <paramref name="configuration"/> is null.
    /// </exception>
    public static IServiceCollection AddRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ThrowIfNull(services);
        ThrowIfNull(configuration);

        return services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.OnRejected = OverrideRejectionResponse();
            options.GlobalLimiter = CreateFixedWindowPartitionedRateLimiter(configuration);
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

    private static PartitionedRateLimiter<HttpContext> CreateFixedWindowPartitionedRateLimiter(
        IConfiguration configuration)
    {
        var rateLimitingSettings = configuration
            .GetRequiredSection(ConfigurationSections.RateLimiting)
            .Get<RateLimitingSettings>()
            .ThrowIfNull();

        return PartitionedRateLimiter.Create<HttpContext, RateLimiterPartitionKey>(httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
                partitionKey: CreateRateLimiterPartitionKey(httpContext),
                factory: partition => new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    Window = TimeSpan.FromSeconds(rateLimitingSettings.WindowSizeInSeconds),
                    QueueLimit = 0,
                    PermitLimit = partition.IsAuthenticated
                        ? rateLimitingSettings.AuthenticatedPermitLimit
                        : rateLimitingSettings.AnonymousPermitLimit
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