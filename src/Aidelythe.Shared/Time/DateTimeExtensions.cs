namespace Aidelythe.Shared.Time;

/// <summary>
/// Provides extension methods for date and time.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Determines whether the specified UTC date and time is less than or equal to the current UTC time.
    /// </summary>
    /// <param name="utcDateTime">The UTC date and time to check.</param>
    /// <returns>
    /// A boolean indicating whether the specified UTC date and time is less than or equal to the current UTC time.
    /// </returns>
    /// <exception cref="ArgumentException">The <paramref name="utcDateTime"/> is not in UTC.</exception>
    public static bool IsNowOrPastUtc(this DateTime utcDateTime)
    {
        ThrowIfNotUtc(utcDateTime);

        return utcDateTime <= DateTime.UtcNow;
    }

    /// <summary>
    /// Returns the number of seconds until the specified UTC date and time.
    /// </summary>
    /// <param name="utcDateTime">The target UTC date and time.</param>
    /// <returns>
    /// The number of seconds until the specified UTC date and time;
    /// or 0 if the specified date and time is in the past.
    /// </returns>
    /// <exception cref="ArgumentException">The <paramref name="utcDateTime"/> is not in UTC.</exception>
    public static int GetSecondsUntilNowUtc(this DateTime utcDateTime)
    {
        ThrowIfNotUtc(utcDateTime);

        var difference = utcDateTime - DateTime.UtcNow;
        if (difference <= TimeSpan.Zero)
            return 0;

        return (int)difference.TotalSeconds;
    }
}