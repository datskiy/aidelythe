using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Results;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users;
using Aidelythe.Domain.Identity.Users.ValueObjects;
using Aidelythe.Shared.Nullable;

namespace Aidelythe.Application._System.Authentication.Handlers;

/// <summary>
/// Represents a command handler for registering a user.
/// </summary>
public sealed partial class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResult>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPasswordService _passwordService;

    private readonly IUserRepository _userRepository;
    private readonly IUserCredentialsRepository _userCredentialsRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterHandler"/> class.
    /// </summary>
    /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
    /// <param name="unitOfWork">The instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="passwordService">The instance of <see cref="IPasswordService"/>.</param>
    /// <param name="userRepository">The instance of <see cref="IUserRepository"/>.</param>
    /// <param name="userCredentialsRepository">The instance of <see cref="IUserCredentialsRepository"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="logger"/>, <paramref name="unitOfWork"/>, <paramref name="passwordService"/>,
    /// <paramref name="userRepository"/> or <paramref name="userCredentialsRepository"/> is null.
    /// </exception>
    public RegisterHandler(
        ILogger<RegisterHandler> logger,
        IUnitOfWork unitOfWork,
        IPasswordService passwordService,
        IUserRepository userRepository,
        IUserCredentialsRepository userCredentialsRepository)
    {
        ThrowIfNull(logger);
        ThrowIfNull(unitOfWork);
        ThrowIfNull(passwordService);
        ThrowIfNull(userRepository);
        ThrowIfNull(userCredentialsRepository);

        _logger = logger;
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
        _userRepository = userRepository;
        _userCredentialsRepository = userCredentialsRepository;
    }

    /// <summary>
    /// Handles the given <see cref="RegisterCommand"/>.
    /// </summary>
    /// <param name="request">The command to register a user.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of the user registration.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<RegisterResult> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: ask and add distributed locking

        if (request.Email is null && request.PhoneNumber is null)
            return new MissingContactMethod();

        var email = request.Email.IfNotNull(email => new Email(email));
        var phoneNumber = request.PhoneNumber.IfNotNull(phoneNumber => new PhoneNumber(phoneNumber));

        var isUserAlreadyRegistered = await _userCredentialsRepository.ExistsByEmailOrPhoneNumberAsync(
            email,
            phoneNumber,
            cancellationToken);

        if (isUserAlreadyRegistered)
            return new AlreadyExists();

        var user = User.Register();
        await _userRepository.AddAsync(user, cancellationToken);

        var password = new Password(request.Password);
        var passwordHash = _passwordService.Hash(password);

        var userCredentials = new UserCredentials(
            user.Id,
            email,
            phoneNumber,
            passwordHash);

        await _userCredentialsRepository.AddAsync(userCredentials, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        LogUserRegistered(user.Id);
        return user.Id.Value;
    }

    [LoggerMessage(LogLevel.Information, "User {UserId} successfully registered")]
    partial void LogUserRegistered(UserId userId);
}