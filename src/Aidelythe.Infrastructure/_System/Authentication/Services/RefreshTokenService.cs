using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.Services;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;
using Aidelythe.Shared.Time;

namespace Aidelythe.Infrastructure._System.Authentication.Services;

/// <summary>
/// Represents a service for managing refresh tokens.
/// </summary>
public sealed class RefreshTokenService : IRefreshTokenService
{
    private readonly IRefreshTokenGrantRepository _refreshTokenGrantRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenService"/> class.
    /// </summary>
    /// <param name="refreshTokenGrantRepository">The instance of <see cref="IRefreshTokenGrantRepository"/>.</param>
    public RefreshTokenService(IRefreshTokenGrantRepository refreshTokenGrantRepository)
    {
        ThrowIfNull(refreshTokenGrantRepository);

        _refreshTokenGrantRepository = refreshTokenGrantRepository;
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

        var refreshTokenHash = HashToken(tokenBytes);
        var expiresAt = DateTime.UtcNow.AddSeconds(expiresIn);

        var refreshTokenGrant = await _refreshTokenGrantRepository.GetAsync(userId, cancellationToken);
        if (refreshTokenGrant is null)
        {
            var newRefreshTokenGrant = new RefreshTokenGrant(userId, refreshTokenHash, expiresAt);

            await _refreshTokenGrantRepository.AddAsync(newRefreshTokenGrant, cancellationToken);
            return new TokenInfo(token, expiresIn);
        }

        refreshTokenGrant.UpdateTokenHash(refreshTokenHash, expiresAt);

        await _refreshTokenGrantRepository.UpdateAsync(refreshTokenGrant, cancellationToken);
        return new TokenInfo(token, expiresIn);
    }

    /// <inheritdoc/>
    public async Task<OneOf<UserId, Expired, NotFound>> ValidateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken)
    {
        var tokenBytes = TryGetBytesFromBase64(refreshToken.Value);
        if (tokenBytes is null)
            return new NotFound();

        var refreshTokenHash = HashToken(tokenBytes);

        var refreshTokenGrant = await _refreshTokenGrantRepository.GetAsync(refreshTokenHash, cancellationToken);
        if(refreshTokenGrant is null)
            return new NotFound();

        if(refreshTokenGrant.ExpiresAt.IsInPastUtc())
            return new Expired();

        return refreshTokenGrant.UserId;
    }

    private static RefreshTokenHash HashToken(byte[] tokenBytes)
    {
        var hashBytes = SHA256.HashData(tokenBytes);
        var hash = Convert.ToBase64String(hashBytes);

        return new RefreshTokenHash(hash);
    }

    private static byte[]? TryGetBytesFromBase64(string base64)
    {
        try
        {
            return Convert.FromBase64String(base64);
        }
        catch (FormatException)
        {
            return null;
        }
    }
}