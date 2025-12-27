namespace Aidelythe.Shared.Guards;

/// <summary>
/// Provides helper methods for enforcing guard clauses.
/// </summary>
public static class GuardHelper
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the specified string is null, empty,
    /// or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.
    /// This will be automatically populated with the calling argument's name.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the specified string is null, empty, or consists only of white-space characters.
    /// </exception>
    public static void ThrowIfNullOrWhiteSpace(
        string? value,
        [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The value cannot be null, empty or whitespace.", paramName);
    }
}