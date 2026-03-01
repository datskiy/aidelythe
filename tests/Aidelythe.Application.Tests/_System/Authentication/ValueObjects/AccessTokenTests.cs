using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.ValueObjects;

public sealed class AccessTokenTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new AccessToken(nullValue!);
        var tryCreateWithEmptyValue = () => new AccessToken(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new AccessToken(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnAccessToken()
    {
        // Arrange
        var value = "access-token";

        // Act
        var accessToken = new AccessToken(value);

        // Assert
        Assert.True(accessToken is not null);
    }

    [Fact]
    public void ToString_ShouldReturnPlaceholderString()
    {
        // Arrange
        var accessToken = new AccessToken("access-token");

        // Act
        var accessTokenString = accessToken.ToString();

        // Assert
        Assert.NotEqual(accessTokenString, accessToken.Value);
    }
}