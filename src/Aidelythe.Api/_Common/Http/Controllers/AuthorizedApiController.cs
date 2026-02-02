using Aidelythe.Api._System.Authentication.Services;

namespace Aidelythe.Api._Common.Http.Controllers;

/// <summary>
/// Represents a base controller for authorized API endpoints.
/// </summary>
[Authorize]
public abstract class AuthorizedApiController : BaseApiController
{
    /// <summary>
    /// Gets the unique identifier of the current user.
    /// </summary>
    protected Guid CurrentUserId
    {
        get
        {
            var userSessionContextAccessor = HttpContext.RequestServices
                .GetRequiredService<IUserSessionContextAccessor>();

            return userSessionContextAccessor.UserSessionContext?.UserId
               ?? throw new InvalidOperationException(
                   "User session context is not available for unauthenticated requests.");
        }
    }
}