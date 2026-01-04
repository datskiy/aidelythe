namespace Aidelythe.Shared.Time;

/// <summary>
/// Provides extension methods for date and time.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Determines whether the specified UTC date and time is in the past
    /// relative to the current UTC time.
    /// </summary>
    /// <param name="utcDateTime">The UTC date and time to check.</param>
    /// <returns>
    /// A boolean indicating whether the specified UTC date and time is in the past.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the specified date and time is not in UTC.</exception>
    public static bool IsInPastUtc(this DateTime utcDateTime)
    {
        ThrowIfNotUtc(utcDateTime);

        return utcDateTime < DateTime.UtcNow;
    }
}