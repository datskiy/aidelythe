using Aidelythe.Shared.Strings;

namespace Aidelythe.Shared.Tests.Strings;

public sealed class StringExtensionsTests
{
    [Fact]
    public void MaskMiddle_WhenStringIsNull_ShouldReturnEmptyString()
    {
        // Arrange
        var nullStr = (string?)null;

        // Act
        var result = nullStr.MaskMiddle();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void MaskMiddle_WhenStringIsEmpty_ShouldReturnEmptyString()
    {
        // Arrange
        var emptyStr = string.Empty;

        // Act
        var result = emptyStr.MaskMiddle();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void MaskMiddle_WhenStringLengthIsGreaterThanVisibleParts_ShouldReturnStringWithMiddleMask()
    {
        // Arrange
        var str = "1234567890";

        // Act
        var result = str.MaskMiddle();

        // Assert
        Assert.Equal("123*****890", result);
    }

    [Fact]
    public void MaskMiddle_WhenStringLengthEqualsVisiblePartsSum_ShouldReturnStringWithBothEndsAndMiddleMask()
    {
        // Arrange
        var str = "abcdef";

        // Act
        var result = str.MaskMiddle(visiblePrefixLength: 3, visibleSuffixLength: 3);

        // Assert
        Assert.Equal("abc*****def", result);
    }

    [Fact]
    public void MaskMiddle_WhenStringLengthIsLessThanVisiblePartsSum_ShouldReturnStringWithVisiblePrefixAndMask()
    {
        // Arrange
        var str = "abcd";

        // Act
        var result = str.MaskMiddle(visiblePrefixLength: 3, visibleSuffixLength: 3);

        // Assert
        Assert.Equal("abc*****", result);
    }

    [Fact]
    public void MaskMiddle_WhenStringIsShorterThanVisiblePrefix_ShouldReturnWholeStringAndMask()
    {
        // Arrange
        var str = "ab";

        // Act
        var result = str.MaskMiddle(visiblePrefixLength: 3, visibleSuffixLength: 3);

        // Assert
        Assert.Equal("ab*****", result);
    }

    [Fact]
    public void MaskMiddle_WhenCustomMaskSettingsAreProvided_ShouldReturnStringWithSettingsUsed()
    {
        // Arrange
        var str = "1234567890";

        // Act
        var result = str.MaskMiddle(
            visiblePrefixLength: 2,
            visibleSuffixLength: 2,
            maskChar: '#',
            maskCount: 3);

        // Assert
        Assert.Equal("12###90", result);
    }

    [Fact]
    public void MaskMiddle_WhenVisiblePrefixLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "123456";

        // Act
        var tryMaskMiddle = () => str.MaskMiddle(visiblePrefixLength: -1);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryMaskMiddle);
    }

    [Fact]
    public void MaskMiddle_WhenVisibleSuffixLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "123456";

        // Act
        var tryMaskMiddle = () => str.MaskMiddle(visibleSuffixLength: -1);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryMaskMiddle);
    }

    [Fact]
    public void MaskMiddle_WhenMaskCountIsZero_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "123456";

        // Act
        var tryMaskMiddle = () => str.MaskMiddle(maskCount: 0);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryMaskMiddle);
    }

    [Fact]
    public void MaskMiddle_WhenMaskCountIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "123456";

        // Act
        var tryMaskMiddle = () => str.MaskMiddle(maskCount: -1);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryMaskMiddle);
    }

    [Fact]
    public void MaskEnding_WhenStringLengthIsGreaterThanVisiblePrefix_ShouldReturnStringWithEndingMask()
    {
        // Arrange
        var str = "1234567890";

        // Act
        var result = str.MaskEnding();

        // Assert
        Assert.Equal("123*****", result);
    }

    [Fact]
    public void MaskEnding_WhenStringIsShorterThanVisiblePrefix_ShouldReturnWholeStringAndMask()
    {
        // Arrange
        var str = "ab";

        // Act
        var result = str.MaskEnding(visiblePrefixLength: 3);

        // Assert
        Assert.Equal("ab*****", result);
    }

    [Fact]
    public void MaskEnding_WhenCustomMaskSettingsAreProvided_ShouldReturnStringWithSettingsUsed()
    {
        // Arrange
        var str = "abcdef";

        // Act
        var result = str.MaskEnding(visiblePrefixLength: 2, maskChar: '#', maskCount: 4);

        // Assert
        Assert.Equal("ab####", result);
    }

    [Fact]
    public void MaskEnding_WhenVisiblePrefixLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "123456";

        // Act
        var tryMaskEnding = () => str.MaskEnding(visiblePrefixLength: -1);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryMaskEnding);
    }

    [Fact]
    public void MaskEnding_WhenMaskCountIsZero_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "123456";

        // Act
        var tryMaskEnding = () => str.MaskEnding(maskCount: 0);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryMaskEnding);
    }
}