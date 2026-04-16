using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents an access token.
/// </summary>
public sealed record AccessToken : SecureValueString
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccessToken"/> class.
    /// </summary>
    /// <param name="value">The access token value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public AccessToken(string value) : base(value)
    {
    }
}