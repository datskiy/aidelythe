namespace Aidelythe.Application._System.Authentication.ValueObjects;

/// <summary>
/// Represents the unique identifier of a user session.
/// </summary>
/// <param name="Value">The unique identifier of the user session.</param>
public readonly record struct UserSessionId(Guid Value) // TODO: switch to source generator
{
    /// <summary>
    /// Generates a new unique identifier of a user session.
    /// </summary>
    /// <returns>
    /// A unique identifier of a user session.
    /// </returns>
    public static UserSessionId New()
    {
        return new UserSessionId(Guid.CreateVersion7());
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Value}";
    }
}