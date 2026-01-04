using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for refresh tokens.
/// </summary>
public interface IRefreshTokenGrantRepository // TODO: use GenericRepository
{
    /// <summary>
    /// Gets the refresh token grant for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to look up.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the refresh token grant for the specified user, or null if no such token exists.
    /// </returns>
    Task<RefreshTokenGrant?> GetAsync(
        UserId userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Gets the refresh token grant associated with the specified refresh token hash.
    /// </summary>
    /// <param name="refreshTokenHash">The refresh token hash to look up.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the refresh token grant associated with the specified refresh token hash,
    /// or null if no such token exists.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="refreshTokenHash"/> is null.</exception>
    Task<RefreshTokenGrant?> GetAsync(
        RefreshTokenHash refreshTokenHash,
        CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified refresh token grant.
    /// </summary>
    /// <param name="refreshTokenGrant">The refresh token grant to add.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="refreshTokenGrant"/> is null.</exception>
    Task AddAsync(
        RefreshTokenGrant refreshTokenGrant,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates the specified refresh token grant.
    /// </summary>
    /// <param name="refreshTokenGrant">The refresh token grant to update.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="refreshTokenGrant"/> is null.</exception>
    Task UpdateAsync(
        RefreshTokenGrant refreshTokenGrant,
        CancellationToken cancellationToken);
}
