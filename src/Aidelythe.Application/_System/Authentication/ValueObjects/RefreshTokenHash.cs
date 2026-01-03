namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a hashed refresh token.
/// </summary>
public sealed record RefreshTokenHash
{
    /// <summary>
    /// Gets the hashed refresh token value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenHash"/> class.
    /// </summary>
    /// <param name="value">The hashed refresh token value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public RefreshTokenHash(string value)
    {
        ThrowIfNullOrWhiteSpace(value);

        Value = value;
    }

    /// <inheritdoc/>
    public override string ToString() // TODO: mb make a base class for such types?
    {
        return "[REDACTED]";
    }
}