using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Handlers;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.Handlers;

public sealed class LogoutAllHandlerTests
{
    private readonly ILogger<LogoutAllHandler> _logger = Substitute.For<ILogger<LogoutAllHandler>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly IUserSessionRepository _userSessionRepository = Substitute.For<IUserSessionRepository>();

    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task LogoutAll_WhenCommandIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullCommand = (LogoutAllCommand?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullCommand!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    [Fact]
    public async Task LogoutAll_WhenUserHasNoActiveSessions_ShouldDoNothing()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateLogoutAllCommandStub();

        _userSessionRepository
            .CountAsync(Arg.Any<UserId>(), Arg.Any<CancellationToken>())
            .Returns(0);

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
    public async Task LogoutAll_WhenUserHasActiveSessions_ShouldDeleteThem()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateLogoutAllCommandStub();

        _userSessionRepository
            .CountAsync(Arg.Any<UserId>(), Arg.Any<CancellationToken>())
            .Returns(3);

        // Act
        await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        await _userSessionRepository
            .Received(requiredNumberOfCalls: 1)
            .DeleteAsync(Arg.Any<UserId>(), Arg.Any<CancellationToken>());

        await _unitOfWork
            .Received(requiredNumberOfCalls: 1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }


    private LogoutAllHandler CreateSut()
    {
        return new LogoutAllHandler(
            _logger,
            _unitOfWork,
            _userSessionRepository);
    }

    private static LogoutAllCommand CreateLogoutAllCommandStub()
    {
        return new LogoutAllCommand(userId: Guid.CreateVersion7());
    }
}