using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Data;

/// <summary>
/// Represents a refresh token descriptor.
/// </summary>
public sealed class RefreshTokenDescriptor
{
    /// <summary>
    /// Gets the refresh token.
    /// </summary>
    public RefreshToken Token { get; }

    /// <summary>
    /// Gets the hashed refresh token.
    /// </summary>
    public RefreshTokenHash Hash { get; }

    /// <summary>
    /// Gets the date and time when the refresh token expires.
    /// </summary>
    public DateTime ExpiresAt { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenDescriptor"/> class.
    /// </summary>
    /// <param name="token">The refresh token.</param>
    /// <param name="hash">The hashed refresh token.</param>
    /// <param name="expiresAt">The date and time when the refresh token expires.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="token"/> or <paramref name="hash"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">The <paramref name="expiresAt"/> is not in UTC.</exception>
    public RefreshTokenDescriptor(
        RefreshToken token,
        RefreshTokenHash hash,
        DateTime expiresAt)
    {
        ThrowIfNull(token);
        ThrowIfNull(hash);
        ThrowIfNotUtc(expiresAt);

        Token = token;
        Hash = hash;
        ExpiresAt = expiresAt;
    }
}