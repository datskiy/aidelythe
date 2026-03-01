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

public sealed class LoginHandlerTests
{
    private readonly ILogger<LoginHandler> _logger = Substitute.For<ILogger<LoginHandler>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly IPasswordService _passwordService = Substitute.For<IPasswordService>();
    private readonly IRefreshTokenService _refreshTokenService = Substitute.For<IRefreshTokenService>();
    private readonly IAccessTokenService _accessTokenService = Substitute.For<IAccessTokenService>();

    private readonly IUserCredentialsRepository _userCredentialsRepository = Substitute.For<IUserCredentialsRepository>();
    private readonly IUserSessionRepository _userSessionRepository = Substitute.For<IUserSessionRepository>();

    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Handle_WhenCommandIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullCommand = (LoginCommand?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullCommand!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    [Fact]
    public async Task Handle_WhenLoginIsUnknown_ShouldReturnInvalidCredentials()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateLoginCommandStub();

        _userCredentialsRepository
            .GetAsync(Arg.Any<Email>(), Arg.Any<CancellationToken>())
            .Returns((UserCredentials?)null);

        // Act
        var result = await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        Assert.IsType<InvalidCredentials>(result.Union.Value);

        await _unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenPasswordIsInvalid_ShouldReturnInvalidCredentials()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateLoginCommandStub();

        _userCredentialsRepository
            .GetAsync(Arg.Any<Email>(), Arg.Any<CancellationToken>())
            .Returns(CreateUserCredentialsStub());

        _passwordService
            .Verify(Arg.Any<Password>(), Arg.Any<PasswordHash>())
            .Returns(new Failure());

        // Act
        var result = await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        Assert.IsType<InvalidCredentials>(result.Union.Value);

        await _unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenPasswordRehashIsNeeded_ShouldRehashPasswordAndReturnTokenPairDetails()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateLoginCommandStub();

        _userCredentialsRepository
            .GetAsync(Arg.Any<Email>(), Arg.Any<CancellationToken>())
            .Returns(CreateUserCredentialsStub());

        _passwordService
            .Verify(Arg.Any<Password>(), Arg.Any<PasswordHash>())
            .Returns(new SuccessRehashNeeded());

        _passwordService
            .Hash(Arg.Any<Password>())
            .Returns(CreatePasswordHashStub());

        _refreshTokenService
            .Generate()
            .Returns(CreateRefreshTokenDescriptorStub());

        _accessTokenService
            .Issue(Arg.Any<UserId>(), Arg.Any<UserSessionId>())
            .Returns(CreateAccessTokenDescriptorStub());

        // Act
        var result = await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        Assert.IsType<TokenPairDetails>(result.Union.Value);

        _passwordService
            .Received(requiredNumberOfCalls: 1)
            .Hash(Arg.Any<Password>());

        await _unitOfWork
            .Received(requiredNumberOfCalls: 1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenCredentialsAreCorrect_ShouldReturnTokenPairDetails()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateLoginCommandStub();

        _userCredentialsRepository
            .GetAsync(Arg.Any<Email>(), Arg.Any<CancellationToken>())
            .Returns(CreateUserCredentialsStub());

        _passwordService
            .Verify(Arg.Any<Password>(), Arg.Any<PasswordHash>())
            .Returns(new Success());

        _refreshTokenService
            .Generate()
            .Returns(CreateRefreshTokenDescriptorStub());

        _accessTokenService
            .Issue(Arg.Any<UserId>(), Arg.Any<UserSessionId>())
            .Returns(CreateAccessTokenDescriptorStub());

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

    private LoginHandler CreateSut()
    {
        return new LoginHandler(
            _logger,
            _unitOfWork,
            _passwordService,
            _refreshTokenService,
            _accessTokenService,
            _userCredentialsRepository,
            _userSessionRepository);
    }

    private static LoginCommand CreateLoginCommandStub()
    {
        return new LoginCommand(
            login: "user@example.com",
            password: "admin694201337");
    }

    private static UserCredentials CreateUserCredentialsStub()
    {
        var userId = UserId.New();
        var email = new Email("user@example.com");

        return new UserCredentials(
            userId,
            email,
            phoneNumber: null,
            passwordHash: CreatePasswordHashStub());
    }

    private static PasswordHash CreatePasswordHashStub()
    {
        return new PasswordHash("hashed-password");
    }

    private static RefreshTokenDescriptor CreateRefreshTokenDescriptorStub()
    {
        return new RefreshTokenDescriptor(
            new RefreshToken("refresh-token"),
            new RefreshTokenHash("hashed-refresh-token"),
            expiresAt: DateTime.UtcNow.AddSeconds(1));
    }

    private static AccessTokenDescriptor CreateAccessTokenDescriptorStub()
    {
        return new AccessTokenDescriptor(
            new AccessToken("access-token"),
            expiresAt: DateTime.UtcNow.AddSeconds(1));
    }
}