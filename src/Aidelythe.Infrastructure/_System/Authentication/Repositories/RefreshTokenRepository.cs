using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for refresh tokens.
/// </summary>
public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    /// <inheritdoc/>
    public Task<RefreshToken?> GetAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        // TODO: implement

        return Task.FromResult(new RefreshToken(
            UserId.New(),
            new RefreshTokenHash("hashed-token"),
            DateTime.UtcNow.AddDays(14)))!;
    }

    /// <inheritdoc/>
    public Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(refreshToken);

        // TODO: implement

        return Task.CompletedTask;
    }

    public Task UpdateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(refreshToken);

        // TODO: implement

        return Task.CompletedTask;
    }
}