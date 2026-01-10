namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents an access token.
/// </summary>
public sealed record AccessToken
{
    /// <summary>
    /// Gets the access token value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessToken"/> class.
    /// </summary>
    /// <param name="value">The access token value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public AccessToken(string value)
    {
        ThrowIfNullOrWhiteSpace(value);

        Value = value;
    }

    /// <inheritdoc/>
    public override string ToString() // TODO: mb make a base class for such types?
    {
        return $"[{nameof(AccessToken)}_REDACTED]";
    }
}