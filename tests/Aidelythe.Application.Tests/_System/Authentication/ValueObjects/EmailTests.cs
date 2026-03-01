using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.ValueObjects;

public sealed class EmailTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new Email(nullValue!);
        var tryCreateWithEmptyValue = () => new Email(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new Email(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var longValue = new string('x', Email.MaximumLength + 1);

        // Act
        var tryCreate = () => new Email(longValue);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenValueFormatIsInInvalid_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidFormatValue = "user@example";

        // Act
        var tryCreate = () => new Email(invalidFormatValue);

        // Assert
        Assert.Throws<ArgumentException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnEmail()
    {
        // Arrange
        var value = "user@example.com";

        // Act
        var email = new Email(value);

        // Assert
        Assert.True(email is not null);
    }

    [Fact]
    public void TryParse_WhenValueFormatIsInvalid_ShouldReturnNull()
    {
        // Arrange
        var invalidFormatValue = "user@example";

        // Act
        var email = Email.TryParse(invalidFormatValue);

        // Assert
        Assert.True(email is null);
    }

    [Fact]
    public void TryParse_WhenValueIsValid_ShouldReturnEmail()
    {
        // Arrange
        var value = "user@example.com";

        // Act
        var email = Email.TryParse(value);

        // Assert
        Assert.True(email is not null);
    }
}