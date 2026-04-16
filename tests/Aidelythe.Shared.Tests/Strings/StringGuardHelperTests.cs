using Aidelythe.Shared.Strings;

namespace Aidelythe.Shared.Tests.Strings;

public sealed class StringGuardHelperTests
{
    [Fact]
    public void ThrowIfShorterThan_WhenStringIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullStr = (string?)null;

        // Act
        var tryThrowIfShorterThen = () => StringGuardHelper.ThrowIfShorterThan(
            nullStr!,
            minimumLength: 3);

        // Assert
        Assert.Throws<ArgumentNullException>(tryThrowIfShorterThen);
    }

    [Fact]
    public void ThrowIfShorterThan_WhenMinimumLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "abc";

        // Act
        var tryThrowIfShorterThen = () => StringGuardHelper.ThrowIfShorterThan(
            str,
            minimumLength: -1);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryThrowIfShorterThen);
    }

    [Fact]
    public void ThrowIfShorterThan_WhenStringIsShorterThanMinimumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "ab";

        // Act
        var tryThrowIfShorterThen = () => StringGuardHelper.ThrowIfShorterThan(
            str,
            minimumLength: 3);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryThrowIfShorterThen);
    }

    [Fact]
    public void ThrowIfShorterThan_WhenStringLengthEqualsMinimumLength_ShouldNotThrow()
    {
        // Arrange
        var str = "abc";

        // Act
        var exception = Record.Exception(() => StringGuardHelper.ThrowIfShorterThan(
            str,
            minimumLength: 3));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ThrowIfShorterThan_WhenStringIsLongerThanMinimumLength_ShouldNotThrow()
    {
        // Arrange
        var str = "abcd";

        // Act
        var exception = Record.Exception(() => StringGuardHelper.ThrowIfShorterThan(
            str,
            minimumLength: 3));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ThrowIfLongerThan_WhenStringIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullStr = (string?)null;

        // Act
        var tryThrowIfLongerThen = () => StringGuardHelper.ThrowIfLongerThan(
            nullStr!,
            maximumLength: 3);

        // Assert
        Assert.Throws<ArgumentNullException>(tryThrowIfLongerThen);
    }

    [Fact]
    public void ThrowIfLongerThan_WhenMaximumLengthIsNegative_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "abc";

        // Act
        var tryThrowIfLongerThen = () => StringGuardHelper.ThrowIfLongerThan(
            str,
            maximumLength: -1);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryThrowIfLongerThen);
    }

    [Fact]
    public void ThrowIfLongerThan_WhenStringIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var str = "abcd";

        // Act
        var tryThrowIfLongerThen = () => StringGuardHelper.ThrowIfLongerThan(
            str,
            maximumLength: 3);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryThrowIfLongerThen);
    }

    [Fact]
    public void ThrowIfLongerThan_WhenStringLengthEqualsMaximumLength_ShouldNotThrow()
    {
        // Arrange
        var str = "abc";

        // Act
        var exception = Record.Exception(() => StringGuardHelper.ThrowIfLongerThan(
            str,
            maximumLength: 3));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ThrowIfLongerThan_WhenStringIsShorterThanMaximumLength_ShouldNotThrow()
    {
        // Arrange
        var str = "ab";

        // Act
        var exception = Record.Exception(() => StringGuardHelper.ThrowIfLongerThan(
            str,
            maximumLength: 3));

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void ThrowIfInvalidFormat_WhenStringIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var str = (string)null!;
        var regex = new Regex(@"^\d+$");

        // Act
        var tryThrowIfInvalidFormat = () => StringGuardHelper.ThrowIfInvalidFormat(
            str,
            regex);

        // Assert
        Assert.Throws<ArgumentNullException>(tryThrowIfInvalidFormat);
    }

    [Fact]
    public void ThrowIfInvalidFormat_WhenRegexIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var nullRegex = (Regex?)null;
        var str = "123";

        // Act
        var tryThrowIfInvalidFormat = () => StringGuardHelper.ThrowIfInvalidFormat(
            str,
            nullRegex!);

        // Assert
        Assert.Throws<ArgumentNullException>(tryThrowIfInvalidFormat);
    }

    [Fact]
    public void ThrowIfInvalidFormat_WhenStringDoesNotMatchFormat_ShouldThrowArgumentException()
    {
        // Arrange
        var str = "abc";
        var regex = new Regex(@"^\d+$");

        // Act
        var tryThrowIfInvalidFormat = () => StringGuardHelper.ThrowIfInvalidFormat(
            str,
            regex);

        // Assert
        Assert.Throws<ArgumentException>(tryThrowIfInvalidFormat);
    }

    [Fact]
    public void ThrowIfInvalidFormat_WhenStringMatchesFormat_ShouldNotThrow()
    {
        // Arrange
        var str = "123";
        var regex = new Regex(@"^\d+$");

        // Act
        var exception = Record.Exception(() => StringGuardHelper.ThrowIfInvalidFormat(
            str,
            regex));

        // Assert
        Assert.Null(exception);
    }
}