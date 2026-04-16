using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a hashed password.
/// </summary>
public sealed record PasswordHash : SecureValueString
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordHash"/> class.
    /// </summary>
    /// <param name="value">The hashed password value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public PasswordHash(string value) : base(value)
    {
    }
}