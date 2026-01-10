namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents the unique identifier of a user session.
/// </summary>
/// <param name="Value">The unique identifier of the user session.</param>
public readonly record struct UserSessionId(Guid Value)
{
    /// <summary>
    /// Generates a new unique identifier for a user session.
    /// </summary>
    /// <returns>
    /// A unique identifier of a user session.
    /// </returns>
    public static UserSessionId New() // TODO: unify all value objects, mb they need some base class, mb not..?
    {
        return new UserSessionId(Guid.CreateVersion7());
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(UserSessionId id)
    {
        return id.Value;
    }
}