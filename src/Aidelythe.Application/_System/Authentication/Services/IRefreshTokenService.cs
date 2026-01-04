using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Services;

/// <summary>
/// Represents a service for managing access tokens.
/// </summary>
public interface IRefreshTokenService
{
    /// <summary>
    /// Issues a refresh token for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user for whom the refresh token is being issued.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains information about the issued refresh token.
    /// </returns>
    Task<TokenInfo> IssueAsync(
        UserId userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Validates the specified refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to validate.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of the refresh token validation.
    /// </returns>
    Task<OneOf<UserId, Expired, NotFound>> ValidateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken);
}