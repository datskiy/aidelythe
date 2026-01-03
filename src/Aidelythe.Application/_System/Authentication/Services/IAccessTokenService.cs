using Aidelythe.Application._System.Authentication.Projections;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Services;

/// <summary>
/// Represents a service for managing access tokens.
/// </summary>
public interface IAccessTokenService
{
    /// <summary>
    /// Issues an access token for the specified user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user for whom the access token is being issued.</param>
    /// <returns>
    /// Information about the issued access token.
    /// </returns>
    TokenInfo Issue(UserId userId);
}