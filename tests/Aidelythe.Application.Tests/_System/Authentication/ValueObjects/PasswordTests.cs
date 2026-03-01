using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.ValueObjects;

public sealed class PasswordTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new Password(nullValue!);
        var tryCreateWithEmptyValue = () => new Password(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new Password(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueIsShorterThanMinimumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var shortValue = new string('x', Password.MinimumLength - 1);

        // Act
        var tryCreate = () => new Password(shortValue);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenValueIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var longValue = new string('x', Password.MaximumLength + 1);

        // Act
        var tryCreate = () => new Password(longValue);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnPassword()
    {
        // Arrange
        var value = "admin694201337";

        // Act
        var password = new Password(value);

        // Assert
        Assert.True(password is not null);
    }

    [Fact]
    public void ToString_ShouldReturnPlaceholderString()
    {
        // Arrange
        var password = new Password("admin694201337");

        // Act
        var passwordString = password.ToString();

        // Assert
        Assert.NotEqual(passwordString, password.Value);
    }
}