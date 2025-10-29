using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Domain.Organizing.ValueObjects;

namespace Aidelythe.Application.Organizing.Results;

/// <summary>
/// Represents the result of creating an event.
/// </summary>
public sealed class CreateEventResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes when creating an event.
    /// </summary>
    public OneOf<EventId, DuplicateTitle, InvalidDateRange> Union { get; }

    private CreateEventResult(OneOf<EventId, DuplicateTitle, InvalidDateRange> union)
    {
        Union = union;
    }

    public static implicit operator CreateEventResult(EventId eventId)
    {
        return new CreateEventResult(eventId);
    }

    public static implicit operator CreateEventResult(DuplicateTitle duplicateTitle)
    {
        return new CreateEventResult(duplicateTitle);
    }

    public static implicit operator CreateEventResult(InvalidDateRange invalidDateRange)
    {
        return new CreateEventResult(invalidDateRange);
    }
}