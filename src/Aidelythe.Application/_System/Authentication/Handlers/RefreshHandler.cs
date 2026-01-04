using Aidelythe.Application._Common.Persistence;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Application._System.Authentication.Discriminants;
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
    private readonly ITokenPairService _tokenPairService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshHandler"/> class.
    /// </summary>
    /// <param name="logger">The instance of <see cref="ILogger"/>.</param>
    /// <param name="unitOfWork">The instance of <see cref="IUnitOfWork"/>.</param>
    /// <param name="refreshTokenService">The instance of <see cref="IRefreshTokenService"/>.</param>
    /// <param name="tokenPairService">The instance of <see cref="ITokenPairService"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="logger"/>, <paramref name="unitOfWork"/>, <paramref name="refreshTokenService"/>
    /// or <paramref name="tokenPairService"/> is null.
    /// </exception>
    public RefreshHandler(
        ILogger<RefreshHandler> logger,
        IUnitOfWork unitOfWork,
        IRefreshTokenService refreshTokenService,
        ITokenPairService tokenPairService)
    {
        ThrowIfNull(logger);
        ThrowIfNull(unitOfWork);
        ThrowIfNull(refreshTokenService);
        ThrowIfNull(tokenPairService);

        _logger = logger;
        _unitOfWork = unitOfWork;
        _refreshTokenService = refreshTokenService;
        _tokenPairService = tokenPairService;
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
            async userId =>
            {
                var tokenPair = await _tokenPairService.GenerateAsync(userId, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("User successfully refreshed the token pair: {UserId}", userId);
                return tokenPair;
            },
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
}