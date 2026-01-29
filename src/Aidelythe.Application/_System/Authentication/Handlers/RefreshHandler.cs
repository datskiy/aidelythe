using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Discriminants;
using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Results;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Shared.Strings;
using Aidelythe.Shared.Tasks;

namespace Aidelythe.Application._System.Authentication.Handlers;

/// <summary>
/// Represents a command handler for logging in a user.
/// </summary>
public sealed class RefreshHandler : IRequestHandler<RefreshCommand, RefreshResult>
{
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IAccessTokenService _accessTokenService;

    private readonly IUserSessionRepository _userSessionRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshHandler"/> class.
    /// </summary>
    /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
    /// <param name="unitOfWork">The instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="refreshTokenService">The instance of <see cref="IRefreshTokenService"/>.</param>
    /// <param name="accessTokenService">The instance of <see cref="IAccessTokenService"/>.</param>
    /// <param name="userSessionRepository">The instance of <see cref="IUserSessionRepository"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="logger"/>, <paramref name="unitOfWork"/>, <paramref name="refreshTokenService"/>,
    /// <paramref name="accessTokenService"/> or <paramref name="userSessionRepository"/> is null.
    /// </exception>
    public RefreshHandler(
        ILogger<RefreshHandler> logger,
        IUnitOfWork unitOfWork,
        IRefreshTokenService refreshTokenService,
        IAccessTokenService accessTokenService,
        IUserSessionRepository userSessionRepository)
    {
        ThrowIfNull(logger);
        ThrowIfNull(unitOfWork);
        ThrowIfNull(refreshTokenService);
        ThrowIfNull(accessTokenService);
        ThrowIfNull(userSessionRepository);

        _logger = logger;
        _unitOfWork = unitOfWork;
        _refreshTokenService = refreshTokenService;
        _accessTokenService = accessTokenService;
        _userSessionRepository = userSessionRepository;
    }

    /// <summary>
    /// Handles the given <see cref="RefreshCommand"/>.
    /// </summary>
    /// <param name="request">The command to refresh the token pair of a user.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of refreshing the token pair of a user.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<RefreshResult> Handle(
        RefreshCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        var refreshToken = new RefreshToken(request.RefreshToken);
        var refreshTokenValidationResult = await _refreshTokenService.ValidateAsync(
            refreshToken,
            cancellationToken);

        return await refreshTokenValidationResult.Match<Task<RefreshResult>>(
            async userSession => await RefreshTokensAndSaveChangesAsync(userSession, cancellationToken),
            async expired =>
            {
                _logger.LogInformation(
                    "Refresh attempt failed due to an expired token: {TokenMask}",
                    request.RefreshToken.MaskMiddle());

                return await new InvalidToken().ToTask();
            },
            async notFound =>
            {
                _logger.LogInformation(
                    "Refresh attempt failed due to an invalid token: {TokenMask}",
                    request.RefreshToken.MaskMiddle());

                return await new InvalidToken().ToTask();
            });
    }

    private async Task<TokenPairDetails> RefreshTokensAndSaveChangesAsync(
        UserSession userSession,
        CancellationToken cancellationToken)
    {
        var newRefreshTokenDescriptor = _refreshTokenService.Generate();
        userSession.RotateToken(newRefreshTokenDescriptor.Hash, newRefreshTokenDescriptor.ExpiresAt);

        await _userSessionRepository.UpdateAsync(userSession, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var accessTokenDescriptor = _accessTokenService.Issue(userSession.UserId, userSession.Id);

        _logger.LogInformation("User {UserId} successfully refreshed the token pair", userSession.UserId);
        return TokenPairDetails.Create(newRefreshTokenDescriptor, accessTokenDescriptor);
    }
}