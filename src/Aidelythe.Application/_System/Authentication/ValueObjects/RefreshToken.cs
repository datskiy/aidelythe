namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a refresh token.
/// </summary>
public sealed record RefreshToken
{
    /// <summary>
    /// The maximum acceptable length of the refresh token.
    /// </summary>
    public const int MaximumLength = 128;

    // TODO: enforce

    /// <summary>
    /// Gets the refresh token value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshToken"/> class.
    /// </summary>
    /// <param name="value">The refresh token value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public RefreshToken(string value)
    {
        ThrowIfNullOrWhiteSpace(value);

        Value = value;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[{nameof(RefreshToken)}_REDACTED]";
    }
}