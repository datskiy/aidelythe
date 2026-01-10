using Aidelythe.Application._System.Authentication.Data;
using Aidelythe.Application._System.Authentication.ValueObjects;
using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Application._System.Authentication.Services;

/// <summary>
/// Represents a service for managing access tokens.
/// </summary>
public interface IAccessTokenService
{
    /// <summary>
    /// Issues an access token for the specified user and session.
    /// </summary>
    /// <param name="userId">The unique identifier of the user for whom the access token is being issued.</param>
    /// <param name="userSessionId">
    /// The unique identifier of the user session for which the access token is being issued.
    /// </param>
    /// <returns>
    /// The access token descriptor.
    /// </returns>
    AccessTokenDescriptor Issue(
        UserId userId,
        UserSessionId userSessionId);
}