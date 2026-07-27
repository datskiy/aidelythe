namespace Aidelythe.Shared.Time;

/// <summary>
/// Provides extension methods for date and time.
/// </summary>
public static class DateTimeOffsetExtensions
{
    /// <summary>
    /// Determines whether the specified date and time is less than or equal to the current UTC time.
    /// </summary>
    /// <param name="dateTimeOffset">The date and time to check.</param>
    /// <returns>
    /// A boolean indicating whether the specified date and time is less than or equal to the current UTC time.
    /// </returns>
    public static bool IsNowOrPastUtc(this DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset <= DateTimeOffset.UtcNow;
    }
}