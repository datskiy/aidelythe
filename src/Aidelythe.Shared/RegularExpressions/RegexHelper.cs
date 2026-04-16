namespace Aidelythe.Shared.RegularExpressions;

/// <summary>
/// Provides helper methods for regular expressions.
/// </summary>
public static class RegexHelper
{
    /// <summary>
    /// Creates a new regular expression with the specified pattern,
    /// defaulting to compiled, culture-invariant, and non-backtracking regex options,
    /// and a match timeout of 100 milliseconds.
    /// </summary>
    /// <param name="pattern">The pattern representing the expected format.</param>
    /// <returns>
    /// A new configured regular expression.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// The <paramref name="pattern"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public static Regex CreateConfigured(string pattern)
    {
        ThrowIfNullOrWhiteSpace(pattern);

        return new Regex(
            pattern,
            options: RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.NonBacktracking,
            matchTimeout: TimeSpan.FromMilliseconds(100));
    }
}