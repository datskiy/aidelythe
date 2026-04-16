namespace Aidelythe.Domain.Identity.Users.ValueObjects;

/// <summary>
/// Represents the unique identifier of a user.
/// </summary>
/// <param name="Value">The unique identifier of the user.</param>
public readonly record struct UserId(Guid Value) // TODO: switch to source generator
{
    /// <summary>
    /// Generates a new unique identifier of a user.
    /// </summary>
    /// <returns>
    /// A unique identifier of a user.
    /// </returns>
    public static UserId New()
    {
        return new UserId(Guid.CreateVersion7());
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Value}";
    }
}