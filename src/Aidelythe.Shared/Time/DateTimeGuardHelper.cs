namespace Aidelythe.Shared.Time;

/// <summary>
/// Provides helper methods for enforcing guard clauses on date and time.
/// </summary>
public static class DateTimeGuardHelper
{
    /// <summary>
    /// Throws an <see cref="ArgumentException"/> if the specified date and time is not in UTC.
    /// </summary>
    /// <param name="dateTime">The date and time to validate.</param>
    /// <param name="paramName">The name of the parameter being validated.
    /// This will be automatically populated with the calling argument's name.
    /// </param>
    /// <exception cref="ArgumentException">The <paramref name="dateTime"/> is not in UTC.</exception>
    public static void ThrowIfNotUtc(
        DateTime dateTime,
        [CallerArgumentExpression(nameof(dateTime))] string? paramName = null)
    {
        if (dateTime.Kind is not DateTimeKind.Utc)
            throw new ArgumentException("The value must be in UTC.", paramName);
    }
}