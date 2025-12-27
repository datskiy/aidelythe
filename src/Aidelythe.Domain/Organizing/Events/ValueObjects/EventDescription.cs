namespace Aidelythe.Domain.Organizing.Events.ValueObjects;

/// <summary>
/// Represents the description of an event.
/// </summary>
/// <param name="Value">The description of the event.</param>
public readonly record struct EventDescription(string Value)
{
    /// <summary>
    /// The maximum acceptable length of the description.
    /// </summary>
    public const int MaximumLength = 500;

    // TODO: enforce rules

    public static implicit operator string(EventDescription description) // TODO: make in base class?
    {
        return description.Value;
    }
}