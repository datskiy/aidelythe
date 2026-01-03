namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents an email address.
/// </summary>
public sealed record Email
{
    /// <summary>
    /// The maximum acceptable length of the email address.
    /// </summary>
    public const int MaximumLength = 254;

    /// <summary>
    /// A regular expression pattern representing the email address format.
    /// </summary>
    public const string FormatPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    /// <summary>
    /// A regular expression representing the email address format.
    /// </summary>
    public static readonly Regex FormatRegex = new(
        FormatPattern,
        options: RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.NonBacktracking,
        matchTimeout: TimeSpan.FromMilliseconds(100));

    // TODO: enforce rules

    /// <summary>
    /// Gets the email address value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="value">The email address value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public Email(string value)
    {
        ThrowIfNullOrWhiteSpace(value);

        Value = value;
    }

    /// <summary>
    /// Attempts to parse the given string into an <see cref="Email"/> object.
    /// </summary>
    /// <param name="email">The string representing the email address to parse.</param>
    /// <returns>
    /// An <see cref="Email"/> object if the parsing is successful; otherwise, <c>null</c>.
    /// </returns>
    public static Email? TryParse(string email)
    {
        return
            string.IsNullOrWhiteSpace(email) ||
            email.Length > MaximumLength ||
            !FormatRegex.IsMatch(email)
                ? null
                : new Email(email);
    }
}