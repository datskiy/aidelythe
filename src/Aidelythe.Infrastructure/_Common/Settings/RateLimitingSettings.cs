namespace Aidelythe.Infrastructure._Common.Settings;

/// <summary>
/// Represents settings for configuring rate-limiting.
/// </summary>
public sealed class RateLimitingSettings
{
    /// <summary>
    /// Gets the maximum number of permits allowed for anonymous requests.
    /// </summary>
    [Required]
    public int AnonymousPermitLimit { get; init; }

    /// <summary>
    /// Gets the maximum number of permits allowed for authenticated requests.
    /// </summary>
    [Required]
    public int AuthenticatedPermitLimit { get; init; }

    /// <summary>
    /// Gets the duration of the rate limit window in seconds.
    /// </summary>
    [Required]
    public int WindowSizeInSeconds { get; init; }
}