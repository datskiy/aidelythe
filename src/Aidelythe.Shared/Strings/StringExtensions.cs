namespace Aidelythe.Shared.Strings;

/// <summary>
/// Provides extension methods for strings.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Masks the middle part of the string with a fixed number of mask characters,
    /// revealing only the beginning and (optionally) the ending.
    /// </summary>
    /// <param name="str">The string to be masked.</param>
    /// <param name="visiblePrefixLength">The number of characters to keep at the beginning.</param>
    /// <param name="visibleSuffixLength">The number of characters to keep at the ending when length allows.</param>
    /// <param name="maskChar">The character to use as the mask.</param>
    /// <param name="maskCount">The number of mask characters to insert.</param>
    /// <returns>
    /// A masked string, if the specified string is not null and does not consist only of white-space characters;
    /// otherwise, an empty string.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="visiblePrefixLength"/> or <paramref name="visibleSuffixLength"/> is negative.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="maskCount"/> is negative or zero.</exception>
    public static string MaskMiddle(
        this string? str,
        int visiblePrefixLength = 3,
        int visibleSuffixLength = 3,
        char maskChar = '*',
        int maskCount = 5)
    {
        ThrowIfNegative(visiblePrefixLength);
        ThrowIfNegative(visibleSuffixLength);
        ThrowIfNegativeOrZero(maskCount);

        if (string.IsNullOrEmpty(str))
            return string.Empty;

        var mask = new string(maskChar, maskCount);

        if (str.Length >= visiblePrefixLength + visibleSuffixLength)
            return $"{str[..visiblePrefixLength]}{mask}{str[^visibleSuffixLength..]}";

        var visible = str[..Math.Min(visiblePrefixLength, str.Length)];
        return visible + mask;
    }

    /// <summary>
    /// Masks the ending of a string with a fixed number of mask characters,
    /// revealing only the beginning.
    /// </summary>
    /// <param name="str">The string to be masked.</param>
    /// <param name="visiblePrefixLength">The number of characters to keep at the beginning.</param>
    /// <param name="maskChar">The character to use as the mask.</param>
    /// <param name="maskCount">The number of mask characters to insert.</param>
    /// <returns>
    /// A masked string, if the specified string is not null and does not consist only of white-space characters;
    /// otherwise, an empty string.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="visiblePrefixLength"/> is negative.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="maskCount"/> is negative or zero.</exception>
    public static string MaskEnding(
        this string? str,
        int visiblePrefixLength = 3,
        char maskChar = '*',
        int maskCount = 5)
    {
        return str.MaskMiddle(visiblePrefixLength, visibleSuffixLength: 0, maskChar, maskCount);
    }
}

