using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a password.
/// </summary>
public sealed record Password : SecureValueString
{
    /// <summary>
    /// The minimum acceptable length of the password.
    /// </summary>
    public const int MinimumLength = 12;

    /// <summary>
    /// The maximum acceptable length of the password.
    /// </summary>
    public const int MaximumLength = 128;

    /// <summary>
    /// Initializes a new instance of the <see cref="Password"/> class.
    /// </summary>
    /// <param name="value">The password value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="value"/> is shorter than <see cref="MinimumLength"/>
    /// or longer than <see cref="MaximumLength"/>.
    /// </exception>
    public Password(string value) : base(
        value,
        MinimumLength,
        MaximumLength)
    {
    }
}