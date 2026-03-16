using Aidelythe.Shared.Collections;

namespace Aidelythe.Shared.Tests.Collections;

public sealed class ReadOnlyCollectionExtensionsTests
{
    [Fact]
    public void AsNonEmpty_WhenCollectionIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullCollection = (int[]?)null;

        // Act
        var tryAsNonEmpty = () => nullCollection!.AsNonEmpty();

        // Assert
        Assert.Throws<ArgumentNullException>(tryAsNonEmpty);
    }

    [Fact]
    public void AsNonEmpty_WhenCollectionIsNotEmpty_ShouldReturnNonEmptyCollection()
    {
        // Arrange
        var collection = new[] { 1, 2, 3, 4, 5 };

        // Act
        var nonEmptyCollection = collection.AsNonEmpty();

        // Assert
        Assert.NotNull(nonEmptyCollection);
    }
}