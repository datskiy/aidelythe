namespace Aidelythe.Shared.ValueObjects;

/// <summary>
/// Represents a value object that encapsulates a string with a specific format.
/// </summary>
public abstract record FormattedValueString : ValueString
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SecureValueString"/> class.
    /// </summary>
    /// <param name="value">The encapsulated string of the value object.</param>
    /// <param name="formatRegex">The regular expression representing the expected format.</param>
    /// <param name="minimumLength">The minimum length of the encapsulated string.</param>
    /// <param name="maximumLength">The maximum length of the encapsulated string.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> does not match the expected format.
    /// </exception>
    /// <exception cref="ArgumentNullException">The <paramref name="formatRegex"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="value"/> is shorter than <paramref name="minimumLength"/>
    /// or longer than <paramref name="maximumLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="minimumLength"/> is negative.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="maximumLength"/> is negative or zero.
    /// </exception>
    protected FormattedValueString(
        string value,
        Regex formatRegex,
        int? minimumLength,
        int? maximumLength) : base(
        value,
        minimumLength,
        maximumLength)
    {
        ThrowIfNull(formatRegex);
        ThrowIfInvalidFormat(value, formatRegex);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FormattedValueString"/> class.
    /// </summary>
    /// <param name="value">The encapsulated string of the value object.</param>
    /// <param name="formatRegex">The regular expression representing the expected format.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentNullException">The <paramref name="formatRegex"/> is null.</exception>
    protected FormattedValueString(
        string value,
        Regex formatRegex) : this(
            value,
            formatRegex,
            minimumLength: null,
            maximumLength: null)
    {
    }
}