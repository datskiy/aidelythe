namespace Aidelythe.Api._System.Authentication;

/// <summary>
/// Represents a user session context.
/// </summary>
public sealed class UserSessionContext
{
    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Gets the unique identifier of the user session.
    /// </summary>
    public Guid UserSessionId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserSessionContext"/> class.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="userSessionId">The unique identifier of the user session.</param>
    public UserSessionContext(Guid userId, Guid userSessionId)
    {
        UserId = userId;
        UserSessionId = userSessionId;
    }
}