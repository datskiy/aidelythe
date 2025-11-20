namespace Aidelythe.Domain.Organizing.Events.ValueObjects;

/// <summary>
/// Represents the title of an event.
/// </summary>
/// <param name="Value">The title of the event.</param>
public readonly record struct EventTitle(string Value)
{
    /// <summary>
    /// The maximum acceptable length of the title.
    /// </summary>
    public const int MaximumLength = 100; // TODO: enforce

    public static implicit operator string(EventTitle title) // TODO: make in base class?
    {
        return title.Value;
    }
}