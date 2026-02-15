namespace Aidelythe.Infrastructure._Common.Settings;

/// <summary>
/// Represents settings for configuring refresh tokens.
/// </summary>
public sealed class RefreshTokenSettings
{
    /// <summary>
    /// Gets the byte count of the refresh token.
    /// </summary>
    [Required]
    public int ByteCount { get; init; }

    /// <summary>
    /// Gets the expiration time of the refresh token in seconds.
    /// </summary>
    [Required]
    public int ExpiresInSeconds { get; init; }
}