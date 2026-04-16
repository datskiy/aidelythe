using Aidelythe.Shared.Nullable;

namespace Aidelythe.Shared.Tests.Nullable;

public sealed class NullableExtensionsTests
{
    [Fact]
    public void IfNotNull_WhenMapperIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var obj = new object();
        var nullMapper = (Func<object, string>?)null;

        // Act
        var tryIfNotNull = () => obj.IfNotNull(nullMapper!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryIfNotNull);
    }

    [Fact]
    public void IfNotNull_WhenObjectIsNull_ShouldReturnNull()
    {
        // Arrange
        var obj = (object?)null;
        var mapper = (object x) => x.ToString()!;

        // Act
        var str = obj.IfNotNull(mapper);

        // Assert
        Assert.Null(str);
    }

    [Fact]
    public void IfNotNull_WhenObjectIsNotNull_ShouldReturnMappedObject()
    {
        // Arrange
        var obj = new object();
        var mapper = (object x) => x.ToString()!;

        // Act
        var str = obj.IfNotNull(mapper);

        // Assert
        Assert.Equal(str, obj.ToString());
    }
}