using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;
using Aidelythe.Shared.Time;

namespace Aidelythe.Application._System.Authentication.Data;

/// <summary>
/// Represents a user session.
/// </summary>
public sealed class UserSession
{
    /// <summary>
    /// Gets the unique identifier of the user session.
    /// </summary>
    public UserSessionId Id { get; }

    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public UserId UserId { get; }

    /// <summary>
    /// Gets the hashed refresh token.
    /// </summary>
    public RefreshTokenHash TokenHash { get; private set; }

    /// <summary>
    /// Gets the date and time when the refresh token expires.
    /// </summary>
    public DateTime ExpiresAt { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSession"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the user session.</param>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="tokenHash">The hashed refresh token.</param>
    /// <param name="expiresAt">The date and time when the refresh token expires.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="tokenHash"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="expiresAt"/> is not in UTC.</exception>
    public UserSession(
        UserSessionId id,
        UserId userId,
        RefreshTokenHash tokenHash,
        DateTime expiresAt)
    {
        ThrowIfNull(tokenHash);
        ThrowIfNotUtc(expiresAt);

        Id = id;
        UserId = userId;
        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
    }

    /// <summary>
    /// Creates a new user session.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="tokenHash">The hashed refresh token.</param>
    /// <param name="expiresAt">The date and time when the refresh token expires.</param>
    /// <returns>
    /// The new user session.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="tokenHash"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="expiresAt"/> is not in UTC.</exception>
    public static UserSession Create(
        UserId userId,
        RefreshTokenHash tokenHash,
        DateTime expiresAt)
    {
        return new UserSession(UserSessionId.New(), userId, tokenHash, expiresAt);
    }

    /// <summary>
    /// Determines whether the refresh token is expired.
    /// </summary>
    /// <returns>
    /// A boolean indicating whether the refresh token is expired.
    /// </returns>
    public bool IsTokenExpired()
    {
        return ExpiresAt.IsNowOrPastUtc();
    }

    /// <summary>
    /// Rotates the refresh token.
    /// </summary>
    /// <param name="tokenHash">The new hashed refresh token.</param>
    /// <param name="expiresAt">The date and time when the new refresh token expires.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="tokenHash"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="expiresAt"/> is not in UTC.</exception>
    public void RotateToken(
        RefreshTokenHash tokenHash,
        DateTime expiresAt)
    {
        ThrowIfNull(tokenHash);
        ThrowIfNotUtc(expiresAt);

        TokenHash = tokenHash;
        ExpiresAt = expiresAt;
    }
}