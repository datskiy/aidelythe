namespace Aidelythe.Domain.Organizing.ValueObjects;

/// <summary>
/// Represents the unique identifier of an event.
/// </summary>
/// <param name="Value">The unique identifier of the event.</param>
public readonly record struct EventId(Guid Value)
{
    public static implicit operator Guid(EventId id)
    {
        return id.Value;
    }
}