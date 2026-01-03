using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing refresh tokens.
/// </summary>
public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenService"/> class.
    /// </summary>
    /// <param name="refreshTokenRepository">The instance of <see cref="IRefreshTokenRepository"/>.</param>
    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
    {
        ThrowIfNull(refreshTokenRepository);

        _refreshTokenRepository = refreshTokenRepository;
    }

    /// <inheritdoc/>
    public async Task<TokenInfo> IssueAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        // TODO: get from config as options
        var byteCount = 64;
        var expiresIn = 1209600;

        var tokenBytes = RandomNumberGenerator.GetBytes(byteCount);
        var token = Convert.ToBase64String(tokenBytes);

        var hashBytes = SHA256.HashData(tokenBytes);
        var hash = new RefreshTokenHash(Convert.ToBase64String(hashBytes));

        var expiresAt = DateTime.UtcNow.AddSeconds(expiresIn);

        var refreshToken = await _refreshTokenRepository.GetAsync(userId, cancellationToken);
        if (refreshToken is null)
        {
            var newRefreshToken = new RefreshToken(userId, hash, expiresAt);

            await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
            return new TokenInfo(token, expiresIn);
        }

        refreshToken.UpdateTokenHash(hash, expiresAt);

        await _refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);
        return new TokenInfo(token, expiresIn);
    }
}