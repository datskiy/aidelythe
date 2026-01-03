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
public sealed class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPasswordService _passwordService;
    private readonly IAccessTokenService _accessTokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    private readonly IUserCredentialsRepository _userCredentialsRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginHandler"/> class.
    /// </summary>
    /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
    /// <param name="unitOfWork">The instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="passwordService">The instance of <see cref="IPasswordService"/>.</param>
    /// <param name="accessTokenService">The instance of <see cref="IAccessTokenService"/>.</param>
    /// <param name="refreshTokenService">The instance of <see cref="IRefreshTokenService"/>.</param>
    /// <param name="userCredentialsRepository">The instance of <see cref="IUserCredentialsRepository"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="logger"/>, <paramref name="unitOfWork"/>, <paramref name="passwordService"/>,
    /// <paramref name="accessTokenService"/>, <paramref name="refreshTokenService"/> or
    /// <paramref name="userCredentialsRepository"/> is null.
    /// </exception>
    public LoginHandler(
        ILogger<LoginHandler> logger,
        IUnitOfWork unitOfWork,
        IPasswordService passwordService,
        IAccessTokenService accessTokenService,
        IRefreshTokenService refreshTokenService,
        IUserCredentialsRepository userCredentialsRepository)
    {
        ThrowIfNull(logger);
        ThrowIfNull(unitOfWork);
        ThrowIfNull(passwordService);
        ThrowIfNull(accessTokenService);
        ThrowIfNull(refreshTokenService);
        ThrowIfNull(userCredentialsRepository);

        _logger = logger;
        _unitOfWork = unitOfWork;
        _passwordService = passwordService;
        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
        _userCredentialsRepository = userCredentialsRepository;
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
        if(userCredentials is null)
            return new InvalidCredentials();

        var password = new Password(request.Password);
        var passwordVerificationResult = _passwordService.Verify(
            password,
            userCredentials.PasswordHash);

        return await passwordVerificationResult.Match<Task<LoginResult>>(
            async success => await GenerateTokensAsync(userCredentials.UserId, cancellationToken),
            async successRehashNeeded =>
            {
                var newPasswordHash = _passwordService.Hash(password);
                userCredentials.UpdatePasswordHash(newPasswordHash);
                await _userCredentialsRepository.UpdateAsync(userCredentials, cancellationToken);

                _logger.LogInformation("Password hash rehashed: {UserId}", userCredentials.UserId);
                return await GenerateTokensAsync(userCredentials.UserId, cancellationToken);
            },
            async failure =>
            {
                _logger.LogInformation("Login attempt failed: {Login}", request.Login.Mask());
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

    private async Task<TokenPair> GenerateTokensAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var accessTokenInfo = _accessTokenService.Issue(userId);
        var refreshTokenInfo = await _refreshTokenService.IssueAsync(userId, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User successfully logged in: {UserId}", userId);
        return new TokenPair(accessTokenInfo, refreshTokenInfo);
    }
}