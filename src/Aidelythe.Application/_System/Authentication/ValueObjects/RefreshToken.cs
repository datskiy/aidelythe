using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a refresh token.
/// </summary>
public sealed record RefreshToken : SecureValueString
{
    /// <summary>
    /// The maximum acceptable length of the refresh token.
    /// </summary>
    public const int MaximumLength = 128;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshToken"/> class.
    /// </summary>
    /// <param name="value">The refresh token value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="value"/> is longer than <see cref="MaximumLength"/>.
    /// </exception>
    public RefreshToken(string value) : base(
        value,
        minimumLength: null,
        MaximumLength)
    {
    }
}