using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;

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
    /// The task result contains the user session for the specified user, or null if no such token exists.
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
    /// or null if no such token exists.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="refreshTokenHash"/> is null.</exception>
    Task<UserSession?> GetAsync(
        RefreshTokenHash refreshTokenHash,
        CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified user session.
    /// </summary>
    /// <param name="userSession">The user session to add.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="userSession"/> is null.</exception>
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
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="userSession"/> is null.</exception>
    Task UpdateAsync(
        UserSession userSession,
        CancellationToken cancellationToken);
}
