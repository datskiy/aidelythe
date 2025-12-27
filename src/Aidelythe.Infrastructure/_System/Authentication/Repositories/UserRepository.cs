using Aidelythe.Application._System.Authentication.Repositories;
using Aidelythe.Domain.Identity.Users;

namespace Aidelythe.Infrastructure._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for users.
/// </summary>
public sealed class UserRepository : IUserRepository
{
    /// <inheritdoc/>
    public Task AddAsync(
        User user,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(user);

        // TODO: implement

        return Task.CompletedTask;
    }
}