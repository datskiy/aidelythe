using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.ValueObjects;

public sealed class PasswordHashTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new PasswordHash(nullValue!);
        var tryCreateWithEmptyValue = () => new PasswordHash(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new PasswordHash(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnPasswordHash()
    {
        // Arrange
        var value = "hashed-password";

        // Act
        var passwordHash = new PasswordHash(value);

        // Assert
        Assert.NotNull(passwordHash);
    }

    [Fact]
    public void ToString_ShouldReturnPlaceholderString()
    {
        // Arrange
        var passwordHash = new PasswordHash("hashed-password");

        // Act
        var passwordHashString = passwordHash.ToString();

        // Assert
        Assert.NotEqual(passwordHashString, passwordHash.Value);
    }
}