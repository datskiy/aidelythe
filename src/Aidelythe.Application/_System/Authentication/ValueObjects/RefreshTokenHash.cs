using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a hashed refresh token.
/// </summary>
public sealed record RefreshTokenHash : SecureValueString
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenHash"/> class.
    /// </summary>
    /// <param name="value">The hashed refresh token value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public RefreshTokenHash(string value) : base(value)
    {
    }
}