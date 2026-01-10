using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Data;

/// <summary>
/// Represents an access token descriptor.
/// </summary>
public readonly record struct AccessTokenDescriptor
{
    /// <summary>
    /// Gets the access token.
    /// </summary>
    public AccessToken Token { get; }

    /// <summary>
    /// Gets the date and time when the access token expires.
    /// </summary>
    public DateTime ExpiresAt { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessTokenDescriptor"/> struct.
    /// </summary>
    /// <param name="token">The access token.</param>
    /// <param name="expiresAt">The date and time when the access token expires.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="token"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="expiresAt"/> is not in UTC.</exception>
    public AccessTokenDescriptor(
        AccessToken token,
        DateTime expiresAt)
    {
        ThrowIfNull(token);
        ThrowIfNotUtc(expiresAt);

        Token = token;
        ExpiresAt = expiresAt;
    }
}