using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Handlers;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application.Tests._System.Authentication.Handlers;

public sealed class RegisterHandlerTests
{
    private readonly ILogger<RegisterHandler> _logger = Substitute.For<ILogger<RegisterHandler>>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly IPasswordService _passwordService = Substitute.For<IPasswordService>();

    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IUserCredentialsRepository _userCredentialsRepository = Substitute.For<IUserCredentialsRepository>();

    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    [Fact]
    public async Task Register_WhenCommandIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sut = CreateSut();
        var nullCommand = (RegisterCommand?)null;

        // Act
        var tryHandle = () => sut.Handle(
            nullCommand!,
            _cancellationToken);

        // Assert
        await Assert.ThrowsAsync<ArgumentNullException>(tryHandle);
    }

    [Fact]
    public async Task Register_WhenCredentialsAreEmpty_ShouldReturnMissingContactMethod()
    {
        // Arrange
        var sut = CreateSut();
        var commandWithEmptyCredentials = CreateRegisterCommandStub(useEmptyCredentials: true);

        // Act
        var result = await sut.Handle(
            commandWithEmptyCredentials,
            _cancellationToken);

        // Assert
        Assert.IsType<MissingContactMethod>(result.Union.Value);

        await _unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Register_WhenUserIsAlreadyRegistered_ShouldReturnAlreadyExists()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateRegisterCommandStub();

        _userCredentialsRepository
            .ExistsByEmailOrPhoneNumberAsync(
                Arg.Any<Email>(),
                Arg.Any<PhoneNumber>(),
                Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        Assert.IsType<AlreadyExists>(result.Union.Value);

        await _unitOfWork
            .DidNotReceive()
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Register_WhenUserIsNotAlreadyRegistered_ShouldReturnRegisteredUserId()
    {
        // Arrange
        var sut = CreateSut();
        var command = CreateRegisterCommandStub();

        _userCredentialsRepository
            .ExistsByEmailOrPhoneNumberAsync(
                Arg.Any<Email>(),
                Arg.Any<PhoneNumber>(),
                Arg.Any<CancellationToken>())
            .Returns(false);

        _passwordService
            .Hash(Arg.Any<Password>())
            .Returns(new PasswordHash("hashed-password"));

        // Act
        var result = await sut.Handle(
            command,
            _cancellationToken);

        // Assert
        Assert.IsType<Guid>(result.Union.Value);

        await _unitOfWork
            .Received(requiredNumberOfCalls: 1)
            .SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    private RegisterHandler CreateSut()
    {
        return new RegisterHandler(
            _logger,
            _unitOfWork,
            _passwordService,
            _userRepository,
            _userCredentialsRepository);
    }

    private static RegisterCommand CreateRegisterCommandStub(bool useEmptyCredentials = false)
    {
        var email = useEmptyCredentials
            ? null
            : "user@example.com";

        return new RegisterCommand(
            email,
            phoneNumber: null,
            password: "admin694201337");
    }
}