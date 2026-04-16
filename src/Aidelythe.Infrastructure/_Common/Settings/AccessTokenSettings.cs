namespace Aidelythe.Infrastructure._Common.Settings;

/// <summary>
/// Represents settings for configuring access tokens.
/// </summary>
public sealed class AccessTokenSettings
{
    /// <summary>
    /// Gets the issuer of the access token.
    /// </summary>
    [Required]
    public required string Issuer { get; init; }

    /// <summary>
    /// Gets the audience of the access token.
    /// </summary>
    [Required]
    public required string Audience { get; init; }

    /// <summary>
    /// Gets the signing key of the access token.
    /// </summary>
    [Required]
    public required string SigningKey { get; init; }

    /// <summary>
    /// Gets the expiration time of the access token in seconds.
    /// </summary>
    [Required]
    public int ExpiresInSeconds { get; init; }

    /// <summary>
    /// Gets the clock skew of the access token in seconds.
    /// </summary>
    [Required]
    public int ClockSkewInSeconds { get; init; }
}