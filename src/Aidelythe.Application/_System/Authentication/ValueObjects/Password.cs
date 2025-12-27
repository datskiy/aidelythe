namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a password.
/// </summary>
public sealed record Password
{
    /// <summary>
    /// The minimum acceptable length of the password.
    /// </summary>
    public const int MinimumLength = 12;

    /// <summary>
    /// The maximum acceptable length of the password.
    /// </summary>
    public const int MaximumLength = 128;

    // TODO: enforce rules

    /// <summary>
    /// Gets the password value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Password"/> class.
    /// </summary>
    /// <param name="value">The password value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public Password(string value)
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