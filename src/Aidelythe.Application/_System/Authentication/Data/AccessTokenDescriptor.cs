using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Data;

/// <summary>
/// Represents an access token descriptor.
/// </summary>
public sealed class AccessTokenDescriptor
{
    /// <summary>
    /// Gets the access token.
    /// </summary>
    public AccessToken Token { get; }

    /// <summary>
    /// Gets the date and time when the access token expires.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessTokenDescriptor"/> class.
    /// </summary>
    /// <param name="token">The access token.</param>
    /// <param name="expiresAt">The date and time when the access token expires.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="token"/> is null.</exception>
    public AccessTokenDescriptor(
        AccessToken token,
        DateTimeOffset expiresAt)
    {
        ThrowIfNull(token);

        Token = token;
        ExpiresAt = expiresAt;
    }
}