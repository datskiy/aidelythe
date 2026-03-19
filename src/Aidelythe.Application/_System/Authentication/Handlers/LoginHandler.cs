using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Results;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;
using Aidelythe.Shared.Strings;
using Aidelythe.Shared.Tasks;

namespace Aidelythe.Application._System.Authentication.Handlers;

/// <summary>
/// Represents a command handler for logging in a user.
/// </summary>
public sealed partial class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPasswordService _passwordService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IAccessTokenService _accessTokenService;

    private readonly IUserCredentialsRepository _userCredentialsRepository;
    private readonly IUserSessionRepository _userSessionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginHandler"/> class.
    /// </summary>
    /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
    /// <param name="unitOfWork">The instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="passwordService">The instance of <see cref="IPasswordService"/>.</param>
    /// <param name="refreshTokenService">The instance of <see cref="IRefreshTokenService"/>.</param>
    /// <param name="accessTokenService">The instance of <see cref="IAccessTokenService"/>.</param>
    /// <param name="userCredentialsRepository">The instance of <see cref="IUserCredentialsRepository"/>.</param>
    /// <param name="userSessionRepository">The instance of <see cref="IUserSessionRepository"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="logger"/>, <paramref name="unitOfWork"/>, <paramref name="passwordService"/>,
    /// <paramref name="refreshTokenService"/>, <paramref name="accessTokenService"/>,
    /// <paramref name="userCredentialsRepository"/> or <paramref name="userSessionRepository"/> is null.
    /// </exception>
    public LoginHandler(
        ILogger<LoginHandler> logger,
        IUnitOfWork unitOfWork,
        IPasswordService passwordService,
        IRefreshTokenService refreshTokenService,
        IAccessTokenService accessTokenService,
        IUserCredentialsRepository userCredentialsRepository,
        IUserSessionRepository userSessionRepository)
    {
        ThrowIfNull(logger);
        ThrowIfNull(unitOfWork);
        ThrowIfNull(passwordService);
        ThrowIfNull(refreshTokenService);
        ThrowIfNull(accessTokenService);
        ThrowIfNull(userCredentialsRepository);
        ThrowIfNull(userSessionRepository);

        _logger = logger;
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
        _refreshTokenService = refreshTokenService;
        _accessTokenService = accessTokenService;
        _userCredentialsRepository = userCredentialsRepository;
        _userSessionRepository = userSessionRepository;
    }

    /// <summary>
    /// Handles the given <see cref="LoginCommand"/>.
    /// </summary>
    /// <param name="request">The command to log in a user.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of the user logging in.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<LoginResult> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: ask and add distributed locking

        var userCredentials = await GetUserCredentialsAsync(request.Login, cancellationToken);
        if (userCredentials is null)
        {
            LogInvalidCredentials(request.Login.MaskEnding());
            return new InvalidCredentials();
        }

        var password = new Password(request.Password);
        var passwordVerificationResult = _passwordService.Verify(
            password,
            userCredentials.PasswordHash);

        return await passwordVerificationResult.Match<Task<LoginResult>>(
            async success => await GenerateTokensAndSaveChangesAsync(userCredentials.UserId, cancellationToken),
            async successRehashNeeded =>
            {
                var newPasswordHash = _passwordService.Hash(password);
                userCredentials.UpdatePasswordHash(newPasswordHash);
                await _userCredentialsRepository.UpdateAsync(userCredentials, cancellationToken);

                LogPasswordRehashed(userCredentials.UserId);
                return await GenerateTokensAndSaveChangesAsync(userCredentials.UserId, cancellationToken);
            },
            async failure =>
            {
                LogInvalidPassword(request.Login.MaskEnding());
                return await new InvalidCredentials().ToTask();
            });
    }

    private async Task<UserCredentials?> GetUserCredentialsAsync(
        string login,
        CancellationToken cancellationToken)
    {
        var email = Email.TryParse(login);
        if (email is not null)
            return await _userCredentialsRepository.GetAsync(email, cancellationToken);

        var phoneNumber = PhoneNumber.TryParse(login);
        if (phoneNumber is not null)
            return await _userCredentialsRepository.GetAsync(phoneNumber, cancellationToken);

        return null;
    }

    private async Task<TokenPairDetails> GenerateTokensAndSaveChangesAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var refreshTokenDescriptor = _refreshTokenService.Generate();

        var userSession = UserSession.Create(
            userId,
            refreshTokenDescriptor.Hash,
            refreshTokenDescriptor.ExpiresAt);

        await _userSessionRepository.AddAsync(userSession, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var accessTokenDescriptor = _accessTokenService.Issue(userId, userSession.Id);

        LogUserLoggedIn(userId);
        return TokenPairDetails.Create(refreshTokenDescriptor, accessTokenDescriptor);
    }

    [LoggerMessage(LogLevel.Information, "Login attempt for {LoginMask} failed due to an invalid login")]
    partial void LogInvalidCredentials(string loginMask);

    [LoggerMessage(LogLevel.Information, "Password hash rehashed for {UserId}")]
    partial void LogPasswordRehashed(UserId userId);

    [LoggerMessage(LogLevel.Information, "Login attempt for {LoginMask} failed due to an invalid password")]
    partial void LogInvalidPassword(string loginMask);

    [LoggerMessage(LogLevel.Information, "User {UserId} successfully logged in")]
    partial void LogUserLoggedIn(UserId userId);
}