namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a phone number.
/// </summary>
public sealed record PhoneNumber
{
    /// <summary>
    /// A regular expression pattern representing the phone number format.
    /// </summary>
    public const string FormatPattern = @"^\+?[1-9]\d{7,14}$";

    /// <summary>
    /// A regular expression representing the phone number format.
    /// </summary>
    public static readonly Regex FormatRegex = new(
        FormatPattern,
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.NonBacktracking,
        matchTimeout: TimeSpan.FromMilliseconds(100));

    // TODO: enforce rules

    /// <summary>
    /// Gets the phone number value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
    /// </summary>
    /// <param name="value">The phone number value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public PhoneNumber(string value)
    {
        ThrowIfNullOrWhiteSpace(value);

        Value = value;
    }
}