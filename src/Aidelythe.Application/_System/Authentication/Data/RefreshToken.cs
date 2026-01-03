using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Data;

/// <summary>
/// Represents a refresh token.
/// </summary>
public sealed class RefreshToken
{
    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public UserId UserId { get; }

    /// <summary>
    /// Gets the hashed refresh token.
    /// </summary>
    public RefreshTokenHash TokenHash { get; private set; }

    /// <summary>
    /// Gets the date when the refresh token expires.
    /// </summary>
    public DateTime ExpiresAt { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshToken"/> class.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="tokenHash">The hashed refresh token.</param>
    /// <param name="expiresAt">The date when the refresh token expires.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="tokenHash"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="expiresAt"/> is not in UTC.</exception>
    public RefreshToken(
        UserId userId,
        RefreshTokenHash tokenHash,
        DateTime expiresAt)
    {
        ThrowIfNull(tokenHash);
        ThrowIfNotUtc(expiresAt);

        UserId = userId;
        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
    }

    /// <summary>
    /// Updates the hashed refresh token and the expiration date.
    /// </summary>
    /// <param name="refreshTokenHash">The new hashed refresh token.</param>
    /// <param name="expiresAt">The date when the refresh token expires.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="refreshTokenHash"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="expiresAt"/> is not in UTC.</exception>
    public void UpdateTokenHash(
        RefreshTokenHash refreshTokenHash,
        DateTime expiresAt)
    {
        ThrowIfNull(refreshTokenHash);
        ThrowIfNotUtc(expiresAt);

        TokenHash = refreshTokenHash;
        ExpiresAt = expiresAt;
    }
}