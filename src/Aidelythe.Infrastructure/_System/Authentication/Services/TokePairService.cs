using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing token pairs.
/// </summary>
public sealed class TokenPairService : ITokenPairService
{
    private readonly IAccessTokenService _accessTokenService;
    private readonly IRefreshTokenService _refreshTokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenService"/> class.
    /// </summary>
    /// <param name="accessTokenService">The instance of <see cref="IAccessTokenService"/>.</param>
    /// <param name="refreshTokenService">The instance of <see cref="IRefreshTokenService"/>.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="accessTokenService"/> or
    /// <paramref name="refreshTokenService"/> is null.
    /// </exception>
    public TokenPairService(
        IAccessTokenService accessTokenService,
        IRefreshTokenService refreshTokenService)
    {
        ThrowIfNull(accessTokenService);
        ThrowIfNull(refreshTokenService);

        _accessTokenService = accessTokenService;
        _refreshTokenService = refreshTokenService;
    }

    /// <inheritdoc/>
    public async Task<TokenPair> GenerateAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        var accessTokenInfo = _accessTokenService.Issue(userId);
        var refreshTokenInfo = await _refreshTokenService.IssueAsync(userId, cancellationToken);

        return new TokenPair(accessTokenInfo, refreshTokenInfo);
    }
}