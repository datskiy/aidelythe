namespace Aidelythe.Shared.Strings;

/// <summary>
/// Provides extension methods for strings.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Masks a string, revealing a specified number of characters at the beginning
    /// and replacing the remaining characters with a specified mask character.
    /// </summary>
    /// <param name="value">The string to be masked.</param>
    /// <param name="visiblePrefixLength">
    /// The number of characters to leave unmasked at the beginning of the string.
    /// </param>
    /// <param name="maskChar">The character to use as the mask.</param>
    /// <returns>
    /// A masked string if the specified string is not null and does not consist only of white-space characters;
    /// otherwise, an empty string.
    /// </returns>
    public static string Mask(
        this string? value,
        int visiblePrefixLength = 3,
        char maskChar = '*')
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        if (visiblePrefixLength <= 0)
            return new string(maskChar, value.Length);

        if (value.Length <= visiblePrefixLength)
            return value;

        var prefix = value[..visiblePrefixLength];
        var maskedPart = new string(maskChar, value.Length - visiblePrefixLength);

        return $"{prefix}{maskedPart}";
    }
}