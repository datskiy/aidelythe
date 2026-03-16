using Aidelythe.Shared.Guards;

namespace Aidelythe.Shared.Tests.Guards;

public sealed class GuardExtensionsTests
{
    [Fact]
    public void ThrowIfNull_WhenObjectIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullObj = (object?)null;

        // Act
        var tryThrowIfNull = () => nullObj.ThrowIfNull();

        // Assert
        Assert.Throws<ArgumentNullException>(tryThrowIfNull);
    }

    [Fact]
    public void ThrowIfNull_WhenObjectIsNotNull_ShouldReturnSameObject()
    {
        // Arrange
        var obj = new object();

        // Act
        var objReturned = obj.ThrowIfNull();

        // Assert
        Assert.Equal(obj, objReturned);
    }
}