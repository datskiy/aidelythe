using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for refresh tokens.
/// </summary>
public sealed class RefreshTokenGrantRepository : IRefreshTokenGrantRepository
{
    /// <inheritdoc/>
    public Task<RefreshTokenGrant?> GetAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        // TODO: implement

        return Task.FromResult(new RefreshTokenGrant(
            UserId.New(),
            new RefreshTokenHash("hashed-token"),
            DateTime.UtcNow.AddDays(14)))!;
    }

    public Task<RefreshTokenGrant?> GetAsync(
        RefreshTokenHash refreshTokenHash,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(refreshTokenHash);

        // TODO: implement

        return Task.FromResult(new RefreshTokenGrant(
            UserId.New(),
            refreshTokenHash,
            DateTime.UtcNow.AddDays(14)))!;
    }

    /// <inheritdoc/>
    public Task AddAsync(
        RefreshTokenGrant refreshTokenGrant,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(refreshTokenGrant);

        // TODO: implement

        return Task.CompletedTask;
    }

    public Task UpdateAsync(
        RefreshTokenGrant refreshTokenGrant,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(refreshTokenGrant);

        // TODO: implement

        return Task.CompletedTask;
    }
}