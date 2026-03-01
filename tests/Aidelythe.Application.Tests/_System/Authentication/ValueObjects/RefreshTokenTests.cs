using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.ValueObjects;

public sealed class RefreshTokenTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new RefreshToken(nullValue!);
        var tryCreateWithEmptyValue = () => new RefreshToken(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new RefreshToken(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueIsLongerThanMaximumLength_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var longValue = new string('x', RefreshToken.MaximumLength + 1);

        // Act
        var tryCreate = () => new RefreshToken(longValue);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnRefreshToken()
    {
        // Arrange
        var value = "refresh-token";

        // Act
        var refreshToken = new RefreshToken(value);

        // Assert
        Assert.True(refreshToken is not null);
    }

    [Fact]
    public void ToString_ShouldReturnPlaceholderString()
    {
        // Arrange
        var refreshToken = new RefreshToken("refresh-token");

        // Act
        var refreshTokenString = refreshToken.ToString();

        // Assert
        Assert.NotEqual(refreshTokenString, refreshToken.Value);
    }
}