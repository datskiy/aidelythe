namespace Aidelythe.Domain.Identity.Users.ValueObjects;

/// <summary>
/// Represents the unique identifier of a user.
/// </summary>
/// <param name="Value">The unique identifier of the user.</param>
public readonly record struct UserId(Guid Value)
{
    /// <summary>
    /// Generates a new unique identifier for a user.
    /// </summary>
    /// <returns>
    /// A unique identifier of a user.
    /// </returns>
    public static UserId New() // TODO: unify all value objects, mb they need some base class, mb not..?
    {
        return new UserId(Guid.CreateVersion7());
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString();
    }

    public static implicit operator Guid(UserId id)
    {
        return id.Value;
    }
}