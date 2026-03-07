using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;
using Aidelythe.Infrastructure._Common.Settings;
using Aidelythe.Infrastructure._System.Authentication.Services;

namespace Aidelythe.Infrastructure.Tests._System.Authentication.Services;

public sealed class RefreshTokenServiceTests
{
    private readonly IUserSessionRepository _userSessionRepository = Substitute.For<IUserSessionRepository>();

    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public void Generate_ShouldReturnRefreshTokenDescriptor()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var refreshTokenDescriptor = sut.Generate();

        // Assert
        Assert.True(refreshTokenDescriptor is not null);
    }

    [Fact]
    public async Task Validate_WhenRefreshTokenIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullRefreshToken = (RefreshToken?)null;

        // Act
        var tryValidate = () => sut.ValidateAsync(
            nullRefreshToken!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryValidate);
    }

    [Fact]
    public async Task Validate_WhenRefreshIsInvalid_ShouldReturnNotFound()
    {
        // Arrange
        var sut = CreateSut();
        var invalidRefreshToken = new RefreshToken("invalid-refresh-token");

        // Act
        var result = await sut.ValidateAsync(
            invalidRefreshToken,
            _cancellationToken);

        // Assert
        Assert.IsType<NotFound>(result.Value);
    }

    [Fact]
    public async Task Validate_WhenRefreshTokenIsNotFound_ShouldReturnNotFound()
    {
        // Arrange
        var sut = CreateSut();
        var refreshToken = new RefreshToken("TWFueSBoYW5kcyBtYWtlIGxpZ2h0IHdvcmsu");

        _userSessionRepository
            .GetAsync(Arg.Any<RefreshTokenHash>(), Arg.Any<CancellationToken>())
            .Returns((UserSession?)null);

        // Act
        var result = await sut.ValidateAsync(
            refreshToken,
            _cancellationToken);

        // Assert
        Assert.IsType<NotFound>(result.Value);
    }

    [Fact]
    public async Task Validate_WhenRefreshTokenIsExpired_ShouldReturnExpired()
    {
        // Arrange
        var sut = CreateSut();
        var refreshToken = new RefreshToken("TWFueSBoYW5kcyBtYWtlIGxpZ2h0IHdvcmsu");

        _userSessionRepository
            .GetAsync(Arg.Any<RefreshTokenHash>(), Arg.Any<CancellationToken>())
            .Returns(CreateUserSessionStub(isExpired: true));

        // Act
        var result = await sut.ValidateAsync(
            refreshToken,
            _cancellationToken);

        // Assert
        Assert.IsType<Expired>(result.Value);
    }

    [Fact]
    public async Task Validate_WhenRefreshTokenIsValid_ShouldReturnUserSession()
    {
        // Arrange
        var sut = CreateSut();
        var refreshToken = new RefreshToken("TWFueSBoYW5kcyBtYWtlIGxpZ2h0IHdvcmsu");

        _userSessionRepository
            .GetAsync(Arg.Any<RefreshTokenHash>(), Arg.Any<CancellationToken>())
            .Returns(CreateUserSessionStub());

        // Act
        var result = await sut.ValidateAsync(
            refreshToken,
            _cancellationToken);

        // Assert
        Assert.IsType<UserSession>(result.Value);
    }

    private RefreshTokenService CreateSut()
    {
        var settings = new RefreshTokenSettings
        {
            ByteCount = 64,
            ExpiresInSeconds = 1
        };

        return new RefreshTokenService(
            _userSessionRepository,
            Options.Create(settings));
    }

    private UserSession CreateUserSessionStub(bool isExpired = false)
    {
        return new UserSession(
            UserSessionId.New(),
            UserId.New(),
            new RefreshTokenHash("TWFueSBoYW5kcyBtYWtlIGxpZ2h0IHdvcmsu"),
            DateTime.UtcNow.AddSeconds(isExpired ? -1 : 1));
    }
}