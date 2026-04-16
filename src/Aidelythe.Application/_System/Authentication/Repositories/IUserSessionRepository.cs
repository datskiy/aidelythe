using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for user sessions.
/// </summary>
public interface IUserSessionRepository // TODO: use GenericRepository
{
    /// <summary>
    /// Gets the user session associated with the specified user session identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user session to look up.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the user session associated with the specified user session identifier,
    /// or null if no such user session exists.
    /// </returns>
    Task<UserSession?> GetAsync(
        UserSessionId id,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets the user session associated with the specified refresh token hash.
    /// </summary>
    /// <param name="refreshTokenHash">The refresh token hash to look up.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the user session associated with the specified refresh token hash,
    /// or null if no such user session exists.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="refreshTokenHash"/> is null.</exception>
    Task<UserSession?> GetAsync(
        RefreshTokenHash refreshTokenHash,
        CancellationToken cancellationToken);

    /// <summary>
    /// Counts the total number of user sessions associated with the specified user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose sessions to count.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the total number of user sessions associated with the specified user identifier.
    /// </returns>
    Task<int> CountAsync(
        UserId userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified user session.
    /// </summary>
    /// <param name="userSession">The user session to add.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="userSession"/> is null.</exception>
    Task AddAsync(
        UserSession userSession,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates the specified user session.
    /// </summary>
    /// <param name="userSession">The user session to update.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="userSession"/> is null.</exception>
    Task UpdateAsync(
        UserSession userSession,
        CancellationToken cancellationToken);

    /// <summary>
    /// Deletes the user session associated with the specified user session identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user session to delete.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task DeleteAsync(
        UserSessionId id,
        CancellationToken cancellationToken);

    /// <summary>
    /// Deletes all user sessions associated with the specified user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose sessions to delete.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task DeleteAsync(
        UserId userId,
        CancellationToken cancellationToken);
}
