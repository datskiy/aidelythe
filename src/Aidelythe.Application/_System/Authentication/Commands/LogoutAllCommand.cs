namespace Aidelythe.Application._System.Authentication.Commands;

/// <summary>
/// Represents a command to log out all user sessions.
/// </summary>
public sealed class LogoutAllCommand : IRequest
{
    /// <summary>
    /// Gets the unique identifier of a user.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoutCommand"/> class.
    /// </summary>
    /// <param name="userId">The unique identifier of a user</param>
    public LogoutAllCommand(Guid userId)
    {
        UserId = userId;
    }
}