using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Handlers;
using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.Handlers;

public sealed class RefreshHandlerTests
{
    private readonly ILogger<RefreshHandler> _logger = Substitute.For<ILogger<RefreshHandler>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly IRefreshTokenService _refreshTokenService = Substitute.For<IRefreshTokenService>();
    private readonly IAccessTokenService _accessTokenService = Substitute.For<IAccessTokenService>();

    private readonly IUserSessionRepository _userSessionRepository = Substitute.For<IUserSessionRepository>();

    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Refresh_WhenCommandIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullCommand = (RefreshCommand?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullCommand!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    [Fact]
    public async Task Refresh_WhenRefreshTokenIsInvalidOrUnknown_ShouldReturnInvalidToken()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateRefreshCommandStub();

        _refreshTokenService
            .ValidateAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
            .Returns(new NotFound());

        // Act
        var result = await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        Assert.IsType<InvalidToken>(result.Union.Value);

        await _unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Refresh_WhenRefreshTokenIsExpired_ShouldReturnInvalidToken()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateRefreshCommandStub();

        _refreshTokenService
            .ValidateAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
            .Returns(new Expired());

        // Act
        var result = await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        Assert.IsType<InvalidToken>(result.Union.Value);

        await _unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Refresh_WhenRefreshTokenIsValid_ShouldReturnTokenPairDetails()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateRefreshCommandStub();

        _refreshTokenService
            .ValidateAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
            .Returns(new UserSession(
                UserSessionId.New(),
                UserId.New(),
                new RefreshTokenHash("hashed-refresh-token"),
                DateTime.UtcNow.AddSeconds(1)));

        _refreshTokenService
            .Generate()
            .Returns(new RefreshTokenDescriptor(
                new RefreshToken("refresh-token"),
                new RefreshTokenHash("hashed-refresh-token"),
                expiresAt: DateTime.UtcNow.AddSeconds(1)));

        _accessTokenService
            .Issue(Arg.Any<UserId>(), Arg.Any<UserSessionId>())
            .Returns(new AccessTokenDescriptor(
                new AccessToken("access-token"),
                expiresAt: DateTime.UtcNow.AddSeconds(1)));

        // Act
        var result = await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        Assert.IsType<TokenPairDetails>(result.Union.Value);

        await _unitOfWork
            .Received(requiredNumberOfCalls: 1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private RefreshHandler CreateSut()
    {
        return new RefreshHandler(
            _logger,
            _unitOfWork,
            _refreshTokenService,
            _accessTokenService,
            _userSessionRepository);
    }

    private static RefreshCommand CreateRefreshCommandStub()
    {
        return new RefreshCommand(refreshToken: "refresh-token");
    }
}