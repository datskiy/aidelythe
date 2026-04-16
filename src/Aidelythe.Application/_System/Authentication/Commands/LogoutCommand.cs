namespace Aidelythe.Application._System.Authentication.Commands;

/// <summary>
/// Represents a command to log out the specified user session.
/// </summary>
public sealed class LogoutCommand : IRequest
{
    /// <summary>
    /// Gets the unique identifier of a user session.
    /// </summary>
    public Guid UserSessionId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoutCommand"/> class.
    /// </summary>
    /// <param name="userSessionId">The unique identifier of a user session</param>
    public LogoutCommand(Guid userSessionId)
    {
        UserSessionId = userSessionId;
    }
}