using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Infrastructure._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for user sessions.
/// </summary>
public sealed class UserSessionRepository : IUserSessionRepository
{
    /// <inheritdoc/>
    public Task<UserSession?> GetAsync(
        UserSessionId id,
        CancellationToken cancellationToken)
    {
        // TODO: implement

        return Task.FromResult(new UserSession(
            UserSessionId.New(),
            UserId.New(),
            new RefreshTokenHash("hashed-token"),
            DateTime.UtcNow.AddDays(14)))!;
    }

    public Task<UserSession?> GetAsync(
        RefreshTokenHash refreshTokenHash,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(refreshTokenHash);

        // TODO: implement

        return Task.FromResult(new UserSession(
            UserSessionId.New(),
            UserId.New(),
            refreshTokenHash,
            DateTime.UtcNow.AddDays(14)))!;
    }

    public Task<int> CountAsync(
        UserId userId,
        CancellationToken cancellationToken)
    {
        // TODO: implement

        return Task.FromResult(7);
    }

    /// <inheritdoc/>
    public Task AddAsync(
        UserSession userSession,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(userSession);

        // TODO: implement

        return Task.CompletedTask;
    }

    public Task UpdateAsync(
        UserSession userSession,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(userSession);

        // TODO: implement

        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        UserSessionId id,
        CancellationToken cancellationToken)
    {
        // TODO: implement

        return Task.CompletedTask;
    }

    public Task DeleteAsync(UserId userId, CancellationToken cancellationToken)
    {
        // TODO: implement

        return Task.CompletedTask;
    }
}