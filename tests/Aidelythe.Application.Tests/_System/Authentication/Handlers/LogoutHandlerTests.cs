using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Handlers;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.Handlers;

public sealed class LogoutHandlerTests
{
    private readonly ILogger<LogoutHandler> _logger = Substitute.For<ILogger<LogoutHandler>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly IUserSessionRepository _userSessionRepository = Substitute.For<IUserSessionRepository>();

    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Logout_WhenCommandIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullCommand = (LogoutCommand?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullCommand!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    [Fact]
    public async Task Logout_WhenUserHasNoActiveSession_ShouldDoNothing()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateLogoutCommandStub();

        _userSessionRepository
            .GetAsync(Arg.Any<UserSessionId>(), Arg.Any<CancellationToken>())
            .Returns((UserSession?)null);

        // Act
        await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        await _unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Logout_WhenHasActiveSession_ShouldDeleteIt()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateLogoutCommandStub();

        _userSessionRepository
            .GetAsync(Arg.Any<UserSessionId>(), Arg.Any<CancellationToken>())
            .Returns(new UserSession(
                UserSessionId.New(),
                UserId.New(),
                new RefreshTokenHash("hashed-refresh-token"),
                DateTime.UtcNow.AddSeconds(1)));

        // Act
        await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        await _userSessionRepository
            .Received(requiredNumberOfCalls: 1)
            .DeleteAsync(Arg.Any<UserSessionId>(), Arg.Any<CancellationToken>());

        await _unitOfWork
            .Received(requiredNumberOfCalls: 1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private LogoutHandler CreateSut()
    {
        return new LogoutHandler(
            _logger,
            _unitOfWork,
            _userSessionRepository);
    }

    private static LogoutCommand CreateLogoutCommandStub()
    {
        return new LogoutCommand(userSessionId: Guid.CreateVersion7());
    }
}