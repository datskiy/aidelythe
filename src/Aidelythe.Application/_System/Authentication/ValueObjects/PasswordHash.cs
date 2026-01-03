namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a hashed password.
/// </summary>
public sealed record PasswordHash
{
    /// <summary>
    /// Gets the hashed password value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordHash"/> class.
    /// </summary>
    /// <param name="value">The hashed password value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public PasswordHash(string value)
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