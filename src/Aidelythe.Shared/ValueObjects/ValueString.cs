namespace Aidelythe.Shared.ValueObjects;

/// <summary>
/// Represents a base value object that encapsulates a string.
/// </summary>
public abstract record ValueString : ValueObject<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueString"/> class.
    /// </summary>
    /// <param name="value">The encapsulated string of the value object.</param>
    /// <param name="minimumLength">The minimum length of the encapsulated string.</param>
    /// <param name="maximumLength">The maximum length of the encapsulated string.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="value"/> is shorter than <paramref name="minimumLength"/>
    /// or longer than <paramref name="maximumLength"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="minimumLength"/> is negative.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="maximumLength"/> is negative or zero.
    /// </exception>
    protected ValueString(
        string value,
        int? minimumLength,
        int? maximumLength) : base(value)
    {
        ThrowIfNullOrWhiteSpace(value);

        if (minimumLength.HasValue)
        {
            ThrowIfShorterThan(value, minimumLength.Value);
            ThrowIfNegative(minimumLength.Value);
        }

        if (maximumLength.HasValue)
        {
            ThrowIfLongerThan(value, maximumLength.Value);
            ThrowIfLessThanOrEqual(maximumLength.Value, 0);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValueString"/> class.
    /// </summary>
    /// <param name="value">The encapsulated string of the value object.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="value"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    protected ValueString(string value) : this(
        value,
        minimumLength: null,
        maximumLength: null)
    {
    }
}