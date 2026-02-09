namespace Aidelythe.Shared.Guards;

/// <summary>
/// Provides extension methods for enforcing guard clauses.
/// </summary>
public static class GuardExtensions
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the specified object is null.
    /// Otherwise, returns the current object as a non-nullable.
    /// </summary>
    /// <param name="obj">The nullable reference type to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.
    /// This will be automatically populated with the calling argument's name.
    /// </param>
    /// <typeparam name="T">The type of the object to validate.</typeparam>
    /// <returns>
    /// The current object as non-nullable.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="obj"/> is null.</exception>
    public static T ThrowIfNull<T>(
        this T? obj,
        [CallerArgumentExpression(nameof(obj))] string? paramName = null)
        where T : class
    {
        return obj ?? throw new ArgumentNullException(paramName);
    }

    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if the specified object is null.
    /// Otherwise, returns the current object as a non-nullable.
    /// </summary>
    /// <param name="obj">The nullable value type to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.
    /// This will be automatically populated with the calling argument's name.
    /// </param>
    /// <typeparam name="T">The type of the object to validate.</typeparam>
    /// <returns>
    /// The current object as non-nullable.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="obj"/> is null.</exception>
    public static T ThrowIfNull<T>(
        this T? obj,
        [CallerArgumentExpression(nameof(obj))] string? paramName = null)
        where T : struct
    {
        return obj ?? throw new ArgumentNullException(paramName);
    }
}