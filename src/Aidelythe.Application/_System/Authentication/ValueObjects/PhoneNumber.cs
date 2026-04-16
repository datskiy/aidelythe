using Aidelythe.Shared.RegularExpressions;
using Aidelythe.Shared.ValueObjects;

namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents a phone number.
/// </summary>
public sealed record PhoneNumber : FormattedValueString
{
    /// <summary>
    /// A regular expression pattern representing the phone number format.
    /// </summary>
    public const string FormatPattern = @"^\+?[1-9]\d{7,14}$";

    /// <summary>
    /// A regular expression representing the phone number format.
    /// </summary>
    public static readonly Regex FormatRegex = RegexHelper.CreateConfigured(FormatPattern);

    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
    /// </summary>
    /// <param name="value">The phone number value.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> does not match the expected format.
    /// </exception>
    public PhoneNumber(string value) : base(
        value,
        FormatRegex)
    {
    }

    /// <summary>
    /// Attempts to parse the given string into an <see cref="PhoneNumber"/> object.
    /// </summary>
    /// <param name="phoneNumber">The string representing the phone number to parse.</param>
    /// <returns>
    /// An <see cref="PhoneNumber"/> object if the parsing is successful; otherwise, null.
    /// </returns>
    public static PhoneNumber? TryParse(string phoneNumber)
    {
        return
            string.IsNullOrWhiteSpace(phoneNumber) ||
            !FormatRegex.IsMatch(phoneNumber)
                ? null
                : new PhoneNumber(phoneNumber);
    }
}