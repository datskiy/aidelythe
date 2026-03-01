using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.ValueObjects;

public sealed class RefreshTokenHashTests
{
    [Fact]
    public void Ctor_WhenValueIsNullOrWhiteSpace_ShouldThrowArgumentException()
    {
        // Arrange
        var nullValue = (string?)null;
        var emptyValue = string.Empty;
        var whiteSpaceValue = " ";

        // Act
        var tryCreateWithNullValue = () => new RefreshTokenHash(nullValue!);
        var tryCreateWithEmptyValue = () => new RefreshTokenHash(emptyValue);
        var tryCreateWithWhiteSpaceValue = () => new RefreshTokenHash(whiteSpaceValue);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullValue);
        Assert.Throws<ArgumentException>(tryCreateWithEmptyValue);
        Assert.Throws<ArgumentException>(tryCreateWithWhiteSpaceValue);
    }

    [Fact]
    public void Ctor_WhenValueIsValid_ShouldReturnRefreshTokenHash()
    {
        // Arrange
        var value = "hashed-refresh-token";

        // Act
        var refreshTokenHash = new RefreshTokenHash(value);

        // Assert
        Assert.True(refreshTokenHash is not null);
    }

    [Fact]
    public void ToString_ShouldReturnPlaceholderString()
    {
        // Arrange
        var refreshTokenHash = new RefreshTokenHash("hashed-refresh-token");

        // Act
        var refreshTokenHashString = refreshTokenHash.ToString();

        // Assert
        Assert.NotEqual(refreshTokenHashString, refreshTokenHash.Value);
    }
}