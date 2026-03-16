using Aidelythe.Shared.Collections;

namespace Aidelythe.Shared.Tests.Collections;

public sealed class NonEmptyCollectionTests
{
    [Fact]
    public void Ctor_WhenCollectionIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullCollection = (int[]?)null;

        // Act
        var tryCreate = () => new NonEmptyCollection<int>(nullCollection!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenCollectionIsEmpty_ShouldThrowArgumentException()
    {
        // Arrange
        var emptyCollection = Array.Empty<int>();

        // Act
        var tryCreate = () => new NonEmptyCollection<int>(emptyCollection);

        // Assert
        Assert.Throws<ArgumentException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenCollectionIsNotEmpty_ShouldReturnNonEmptyCollection()
    {
        // Arrange
        var collection = new[] { 1, 2, 3, 4, 5 };

        // Act
        var nonEmptyCollection = new NonEmptyCollection<int>(collection);

        // Assert
        Assert.NotNull(nonEmptyCollection);
    }
}