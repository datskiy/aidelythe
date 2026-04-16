using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.Data;

public sealed class RefreshTokenDescriptorTests
{
    [Fact]
    public void Ctor_WhenRefreshTokenOrRefreshTokenHashIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var refreshToken = new RefreshToken("refresh-token");
        var refreshTokenHash = new RefreshTokenHash("hashed-refresh-token");
        var expiresAtUtc = DateTime.UtcNow.AddSeconds(1);

        var nullRefreshToken = (RefreshToken?)null;
        var nullRefreshTokenHash = (RefreshTokenHash?)null;

        // Act
        var tryCreateWithNullRefreshToken = () => new RefreshTokenDescriptor(
            nullRefreshToken!,
            refreshTokenHash,
            expiresAtUtc);

        var tryCreateWithNullRefreshTokenHash = () => new RefreshTokenDescriptor(
            refreshToken,
            nullRefreshTokenHash!,
            expiresAtUtc);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreateWithNullRefreshToken);
        Assert.Throws<ArgumentNullException>(tryCreateWithNullRefreshTokenHash);
    }

    [Fact]
    public void Ctor_WhenExpiresAtIsNotUtc_ShouldThrowArgumentException()
    {
        // Arrange
        var refreshToken = new RefreshToken("refresh-token");
        var refreshTokenHash = new RefreshTokenHash("hashed-refresh-token");
        var expiresAtLocal = DateTime.Now.AddSeconds(1);

        // Act
        var tryCreate = () => new RefreshTokenDescriptor(
            refreshToken,
            refreshTokenHash,
            expiresAtLocal);

        // Assert
        Assert.Throws<ArgumentException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenArgumentsAreValid_ShouldReturnRefreshTokenDescriptor()
    {
        // Arrange
        var refreshToken = new RefreshToken("refresh-token");
        var refreshTokenHash = new RefreshTokenHash("hashed-refresh-token");
        var expiresAtUtc = DateTime.UtcNow.AddSeconds(1);

        // Act
        var refreshTokenDescriptor = new RefreshTokenDescriptor(
            refreshToken,
            refreshTokenHash,
            expiresAtUtc);

        // Assert
        Assert.NotNull(refreshTokenDescriptor);
    }
}