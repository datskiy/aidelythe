using Aidelythe.Shared.Time;

namespace Aidelythe.Shared.Tests.Time;

public sealed class DateTimeOffsetExtensionsTests
{
    [Fact]
    public void IsNowOrPastUtc_WhenDateAndTimeIsInPast_ShouldReturnTrue()
    {
        // Arrange
        var dateTimeOffset = DateTimeOffset.UtcNow.AddMinutes(-1);

        // Act
        var result = dateTimeOffset.IsNowOrPastUtc();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsNowOrPastUtc_WhenDateAndTimeIsInFuture_ShouldReturnFalse()
    {
        // Arrange
        var dateTimeOffset = DateTimeOffset.UtcNow.AddMinutes(1);

        // Act
        var result = dateTimeOffset.IsNowOrPastUtc();

        // Assert
        Assert.False(result);
    }
}