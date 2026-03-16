using Aidelythe.Shared.Time;

namespace Aidelythe.Shared.Tests.Time;

public sealed class DateTimeGuardHelperTests
{
    [Fact]
    public void ThrowIfNotUtc_WhenDateTimeKindIsNotUtc_ShouldThrowArgumentException()
    {
        // Arrange
        var localDateTime = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Local);

        // Act
        var tryThrowIfNotUtc = () => DateTimeGuardHelper.ThrowIfNotUtc(localDateTime);

        // Assert
        Assert.Throws<ArgumentException>(tryThrowIfNotUtc);
    }

    [Fact]
    public void ThrowIfNotUtc_WhenDateTimeKindIsUtc_ShouldNotThrow()
    {
        // Arrange
        var dateTime = new DateTime(2026, 1, 1, 12, 0, 0, DateTimeKind.Utc);

        // Act
        var exception = Record.Exception(() => DateTimeGuardHelper.ThrowIfNotUtc(dateTime));

        // Assert
        Assert.Null(exception);
    }
}