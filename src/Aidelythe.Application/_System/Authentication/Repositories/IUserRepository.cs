using Aidelythe.Domain.Identity.Users;

namespace Aidelythe.Application._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for users.
/// </summary>
public interface IUserRepository // TODO: use GenericRepository
{
    /// <summary>
    /// Adds the specified user.
    /// </summary>
    /// <param name="user">The user to add.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="user"/> is null.</exception>
    Task AddAsync(
        User user,
        CancellationToken cancellationToken);
}
