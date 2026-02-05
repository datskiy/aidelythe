namespace Aidelythe.Api._System.Bandwidth;

/// <summary>
/// Represents a rate limiter partition key.
/// </summary>
/// <param name="Value">The value used to group requests.</param>
/// <param name="IsAuthenticated">A boolean indicating whether the request is authenticated.</param>
public record struct RateLimiterPartitionKey(string Value, bool IsAuthenticated);