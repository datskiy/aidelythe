using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Services;

/// <summary>
/// Represents a service for managing refresh tokens.
/// </summary>
public interface IRefreshTokenService
{
    /// <summary>
    /// Generates a refresh token.
    /// </summary>
    /// <returns>
    /// The refresh token descriptor.
    /// </returns>
    RefreshTokenDescriptor Generate();

    /// <summary>
    /// Validates the specified refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to validate.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of the refresh token validation.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="refreshToken"/> is null.</exception>
    Task<OneOf<UserSession, Expired, NotFound>> ValidateAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken);
}