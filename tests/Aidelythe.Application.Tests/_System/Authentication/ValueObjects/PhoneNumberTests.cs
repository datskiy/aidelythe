using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.ValueObjects;

public sealed class PhoneNumberTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new PhoneNumber(nullValue!);
        var tryCreateWithEmptyValue = () => new PhoneNumber(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new PhoneNumber(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueFormatIsInInvalid_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidFormatValue = "+35700";

        // Act
        var tryCreate = () => new PhoneNumber(invalidFormatValue);

        // Assert
        Assert.Throws<ArgumentException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnPhoneNumber()
    {
        // Arrange
        var value = "+70123456789";

        // Act
        var phoneNumber = new PhoneNumber(value);

        // Assert
        Assert.True(phoneNumber is not null);
    }

    [Fact]
    public void TryParse_WhenValueFormatIsInvalid_ShouldReturnNull()
    {
        // Arrange
        var invalidFormatValue = "+35700";

        // Act
        var phoneNumber = PhoneNumber.TryParse(invalidFormatValue);

        // Assert
        Assert.True(phoneNumber is null);
    }

    [Fact]
    public void TryParse_WhenValueIsValid_ShouldReturnEmail()
    {
        // Arrange
        var value = "+70123456789";

        // Act
        var phoneNumber = PhoneNumber.TryParse(value);

        // Assert
        Assert.True(phoneNumber is not null);
    }
}