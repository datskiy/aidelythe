using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.Data;

public sealed class UserSessionTests
{
    [Fact]
    public void Ctor_WhenRefreshTokenHashIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var userSessionId = UserSessionId.New();
        var userId = UserId.New();
        var nullRefreshTokenHash = (RefreshTokenHash?)null;
        var expiresAtUtc = DateTime.UtcNow.AddSeconds(1);

        // Act
        var tryCreate = () => new UserSession(
            userSessionId,
            userId,
            nullRefreshTokenHash!,
            expiresAtUtc);

        // Assert
        Assert.Throws<ArgumentNullException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenExpiresAtIsNotUtc_ShouldThrowArgumentException()
    {
        // Arrange
        var userSessionId = UserSessionId.New();
        var userId = UserId.New();
        var refreshTokenHash = new RefreshTokenHash("hashed-refresh-token");
        var expiresAtLocal = DateTime.Now.AddSeconds(1);

        // Act
        var tryCreate = () => new UserSession(
            userSessionId,
            userId,
            refreshTokenHash,
            expiresAtLocal);

        // Assert
        Assert.Throws<ArgumentException>(tryCreate);
    }

    [Fact]
    public void Ctor_WhenArgumentsAreValid_ShouldReturnUserSession()
    {
        // Arrange
        var userSessionId = UserSessionId.New();
        var userId = UserId.New();
        var refreshTokenHash = new RefreshTokenHash("hashed-refresh-token");
        var expiresAtUtc = DateTime.UtcNow.AddSeconds(1);

        // Act
        var userSession = new UserSession(
            userSessionId,
            userId,
            refreshTokenHash,
            expiresAtUtc);

        // Assert
        Assert.NotNull(userSession);
    }

    [Fact]
    public void IsTokenExpired_WhenTokenIsExpired_ShouldReturnTrue()
    {
        // Arrange
        var userSession = new UserSession(
            UserSessionId.New(),
            UserId.New(),
            new RefreshTokenHash("hashed-refresh-token"),
            expiresAt: DateTime.UtcNow.AddSeconds(-1));

        // Act
        var isTokenExpired = userSession.IsTokenExpired();

        // Assert
        Assert.True(isTokenExpired);
    }

    [Fact]
    public void IsTokenExpired_WhenTokenIsNotExpired_ShouldReturnFalse()
    {
        // Arrange
        var userSession = new UserSession(
            UserSessionId.New(),
            UserId.New(),
            new RefreshTokenHash("hashed-refresh-token"),
            expiresAt: DateTime.UtcNow.AddSeconds(1));

        // Act
        var isTokenExpired = userSession.IsTokenExpired();

        // Assert
        Assert.False(isTokenExpired);
    }

    [Fact]
    public void RotateToken_WhenRefreshTokenHashIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var userSession = new UserSession(
            UserSessionId.New(),
            UserId.New(),
            new RefreshTokenHash("hashed-refresh-token"),
            expiresAt: DateTime.UtcNow.AddSeconds(1));

        var nullRefreshTokenHash = (RefreshTokenHash?)null;
        var expiresAtUtc = DateTime.UtcNow.AddSeconds(1);

        // Act
        var tryRotateToken = () => userSession.RotateToken(
            nullRefreshTokenHash!,
            expiresAtUtc);

        // Assert
        Assert.Throws<ArgumentNullException>(tryRotateToken);
    }

    [Fact]
    public void RotateToken_WhenExpiresAtIsNotUtc_ShouldThrowArgumentException()
    {
        // Arrange
        var userSession = new UserSession(
            UserSessionId.New(),
            UserId.New(),
            new RefreshTokenHash("hashed-refresh-token"),
            expiresAt: DateTime.UtcNow.AddSeconds(1));

        var refreshTokenHash = new RefreshTokenHash("hashed-refresh-token");
        var expiresAtLocal = DateTime.Now.AddSeconds(1);

        // Act
        var tryRotateToken = () => userSession.RotateToken(
            refreshTokenHash,
            expiresAtLocal);

        // Assert
        Assert.Throws<ArgumentException>(tryRotateToken);
    }

    [Fact]
    public void RotateToken_WhenArgumentsAreValid_ShouldRotateToken()
    {
        // Arrange
        var userSession = new UserSession(
            UserSessionId.New(),
            UserId.New(),
            new RefreshTokenHash("hashed-refresh-token"),
            expiresAt: DateTime.UtcNow.AddSeconds(1));

        var refreshTokenHash = new RefreshTokenHash("hashed-refresh-token");
        var expiresAtUtc = DateTime.UtcNow.AddSeconds(1);

        // Act
        userSession.RotateToken(
            refreshTokenHash,
            expiresAtUtc);

        // Assert
        Assert.Equal(refreshTokenHash, userSession.TokenHash);
        Assert.Equal(expiresAtUtc, userSession.ExpiresAt);
    }
}