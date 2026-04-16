using Aidelythe.Shared.Guards;

namespace Aidelythe.Api._System.Authentication.Services;

/// <summary>
/// Represents a user session context accessor.
/// </summary>
public sealed class UserSessionContextAccessor : IUserSessionContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <inheritdoc/>
    public UserSessionContext? UserSessionContext { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSessionContextAccessor"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The instance of <see cref="IHttpContextAccessor"/>.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="httpContextAccessor"/> is null.</exception>
    public UserSessionContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        ThrowIfNull(httpContextAccessor);

        _httpContextAccessor = httpContextAccessor;

        UserSessionContext = GetUserSessionContext();
    }

    private UserSessionContext? GetUserSessionContext()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user?.Identity is null || !user.Identity.IsAuthenticated)
            return null;

        var userIdClaim = user
            .FindFirst(ClaimTypes.NameIdentifier)
            .ThrowIfNull();

        var sessionIdClaim = user
            .FindFirst(ClaimTypes.Sid)
            .ThrowIfNull();

        return new UserSessionContext(
            Guid.Parse(userIdClaim.Value),
            Guid.Parse(sessionIdClaim.Value));
    }
}