using Aidelythe.Domain.Identity.Users.ValueObjects;

namespace Aidelythe.Domain.Identity.Users;

/// <summary>
/// Represents a user.
/// </summary>
public sealed class User
{
    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public UserId Id { get; }

    // TODO: implement

    private User(UserId id)
    {
        Id = id;
    }

    /// <summary>
    /// Register a new user.
    /// </summary>
    /// <returns>
    /// The registered user.
    /// </returns>
    public static User Register()
    {
        return new User(UserId.New());
    }
}