using Aidelythe.Shared.Time;

namespace Aidelythe.Shared.Tests.Time;

public sealed class DateTimeExtensionsTests
{
    [Fact]
    public void IsNowOrPastUtc_WhenDateTimeKindIsNotUtc_ShouldThrowArgumentException()
    {
        // Arrange
        var localDateTime = GetLocalDateTime();

        // Act
        var tryIsNowOrPastUtc = () => { localDateTime.IsNowOrPastUtc(); };

        // Assert
        Assert.Throws<ArgumentException>(tryIsNowOrPastUtc);
    }

    [Fact]
    public void IsNowOrPastUtc_WhenDateTimeIsInPast_ShouldReturnTrue()
    {
        // Arrange
        var dateTime = DateTime.UtcNow.AddMinutes(-1);

        // Act
        var result = dateTime.IsNowOrPastUtc();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsNowOrPastUtc_WhenDateTimeIsInFuture_ShouldReturnFalse()
    {
        // Arrange
        var dateTime = DateTime.UtcNow.AddMinutes(1);

        // Act
        var result = dateTime.IsNowOrPastUtc();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetSecondsUntilNowUtc_WhenDateTimeKindIsNotUtc_ShouldThrowArgumentException()
    {
        // Arrange
        var localDateTime = GetLocalDateTime();

        // Act
        var tryGetSecondsUntilNowUtc = () => { localDateTime.GetSecondsUntilNowUtc(); };

        // Assert
        Assert.Throws<ArgumentException>(tryGetSecondsUntilNowUtc);
    }

    [Fact]
    public void GetSecondsUntilNowUtc_WhenDateTimeIsInPast_ShouldReturnZero()
    {
        // Arrange
        var dateTime = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var result = dateTime.GetSecondsUntilNowUtc();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetSecondsUntilNowUtc_WhenDateTimeIsNow_ShouldReturnZero()
    {
        // Arrange
        var dateTime = DateTime.UtcNow;

        // Act
        var result = dateTime.GetSecondsUntilNowUtc();

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetSecondsUntilNowUtc_WhenDateTimeIsInFuture_ShouldReturnSecondsUntilNow()
    {
        // Arrange
        var dateTime = DateTime.UtcNow.AddSeconds(10);

        // Act
        var result = dateTime.GetSecondsUntilNowUtc();

        // Assert
        Assert.InRange(result, 1, 10);
    }

    private static DateTime GetLocalDateTime()
    {
        return new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Local);
    }
}