namespace Aidelythe.Domain.Organizing.Events.ValueObjects;

/// <summary>
/// Represents the unique identifier of an event.
/// </summary>
/// <param name="Value">The unique identifier of the event.</param>
public readonly record struct EventId(Guid Value) // TODO: switch to source generator
{
    /// <summary>
    /// Generates a new unique identifier of an event.
    /// </summary>
    /// <returns>
    /// A unique identifier of an event.
    /// </returns>
    public static EventId New()
    {
        return new EventId(Guid.CreateVersion7());
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Value}";
    }
}