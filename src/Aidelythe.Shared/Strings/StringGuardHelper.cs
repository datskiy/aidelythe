namespace Aidelythe.Shared.Strings;

/// <summary>
/// Provides helper methods for enforcing guard clauses on strings.
/// </summary>
public static class StringGuardHelper
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the specified string is null, empty,
    /// or consists only of white-space characters.
    /// </summary>
    /// <param name="str">The string to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.
    /// This will be automatically populated with the calling argument's name.
    /// </param>
    /// <exception cref="ArgumentException">
    /// The <param name="str"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public static void ThrowIfNullOrWhiteSpace(
        string? str,
        [CallerArgumentExpression(nameof(str))] string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(str))
            throw new ArgumentException("The value cannot be null, empty or whitespace.", paramName);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified string
    /// is shorter than the specified minimum length.
    /// </summary>
    /// <param name="str">The string to validate.</param>
    /// <param name="minimumLength">The minimum length of the string.</param>
    /// <param name="paramName">The name of the parameter being validated.
    /// This will be automatically populated with the calling argument's name.
    /// </param>
    /// <exception cref="ArgumentNullException">The <param name="str"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <param name="str"/> is shorter than the specified minimum length.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">The <param name="minimumLength"/> is negative.</exception>
    public static void ThrowIfShorterThan(
        string str,
        int minimumLength,
        [CallerArgumentExpression(nameof(str))] string? paramName = null)
    {
        ThrowIfNull(str);
        ThrowIfNegative(minimumLength);

        if (str.Length < minimumLength)
            throw new ArgumentOutOfRangeException(
                paramName,
                str.Length,
                $"The value cannot be less than {minimumLength} characters long.");
    }

    /// <summary>
    /// Throws an <see cref="ArgumentOutOfRangeException"/> if the specified string
    /// is longer than the specified maximum length.
    /// </summary>
    /// <param name="str">The string to validate.</param>
    /// <param name="maximumLength">The maximum length of the string.</param>
    /// <param name="paramName">The name of the parameter being validated.
    /// This will be automatically populated with the calling argument's name.
    /// </param>
    /// <exception cref="ArgumentNullException">The <param name="str"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <param name="str"/> is longer than the specified maximum length.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">The <param name="maximumLength"/> is negative.</exception>
    public static void ThrowIfLongerThan(
        string str,
        int maximumLength,
        [CallerArgumentExpression(nameof(str))] string? paramName = null)
    {
        ThrowIfNull(str);
        ThrowIfNegative(maximumLength);

        if (str.Length > maximumLength)
            throw new ArgumentOutOfRangeException(
                paramName,
                str.Length,
                $"The value cannot be more than {maximumLength} characters long.");
    }

    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the specified string does not match the specified format.
    /// </summary>
    /// <param name="str">The string to validate.</param>
    /// <param name="formatRegex">The regular expression representing the expected format.</param>
    /// <param name="paramName">The name of the parameter being validated.
    /// This will be automatically populated with the calling argument's name.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// The <param name="str"/> or <param name="formatRegex"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The <param name="str"/> does not match the specified format.
    /// </exception>
    public static void ThrowIfInvalidFormat(
        string str,
        Regex formatRegex,
        [CallerArgumentExpression(nameof(str))] string? paramName = null)
    {
        ThrowIfNull(str);
        ThrowIfNull(formatRegex);

        if (!formatRegex.IsMatch(str))
            throw new ArgumentException("The value does not match the specified format.", paramName);
    }
}