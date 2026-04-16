using Aidelythe.Shared.RegularExpressions;
using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents an email address.
/// </summary>
public sealed record Email : FormattedValueString
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
    public static readonly Regex FormatRegex = RegexHelper.CreateConfigured(FormatPattern);

    /// <summary>
    /// Initializes a new instance of the <see cref="Email"/> class.
    /// </summary>
    /// <param name="value">The email address value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> does not match the expected format.
    /// </exception>
    public Email(string value) : base(
        value,
        FormatRegex,
        minimumLength: null,
        MaximumLength)
    {
    }

    /// <summary>
    /// Attempts to parse the given string into an <see cref="Email"/> object.
    /// </summary>
    /// <param name="email">The string representing the email address to parse.</param>
    /// <returns>
    /// An <see cref="Email"/> object if the parsing is successful; otherwise, null.
    /// </returns>
    public static Email? TryParse(string email)
    {
        try
        {
            return new Email(email);
        }
        catch
        {
            return null;
        }
    }
}