namespace Aidelythe.Api._System.Authentication.Services;

/// <summary>
/// Represents a user session context accessor.
/// </summary>
public interface IUserSessionContextAccessor
{
    /// <summary>
    /// Gets the current user session context.
    /// </summary>
    /// <remarks>
    /// May be null if no user session is currently active.
    /// </remarks>
    UserSessionContext? UserSessionContext { get; }
}