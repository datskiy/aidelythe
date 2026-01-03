using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Repositories;

/// <summary>
/// Represents a repository for refresh tokens.
/// </summary>
public interface IRefreshTokenRepository // TODO: use GenericRepository
{
    /// <summary>
    /// Gets the refresh token for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to look up.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the refresh token for the specified user, or null if no such token exists.
    /// </returns>
    Task<RefreshToken?> GetAsync(
        UserId userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Adds the specified refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to add.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="refreshToken"/> is null.</exception>
    Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates the specified refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to update.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="refreshToken"/> is null.</exception>
    Task UpdateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken);
}
