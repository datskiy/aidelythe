using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application.Organizing.Projections;

namespace Aidelythe.Application.Organizing.Results;

/// <summary>
/// Represents the result of updating an event.
/// </summary>
public sealed class UpdateEventResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes when updating an event.
    /// </summary>
    public OneOf<EventDetails, NotFound, DuplicateTitle, InvalidDateRange> Union { get; }

    private UpdateEventResult(OneOf<EventDetails, NotFound, DuplicateTitle, InvalidDateRange> union)
    {
        Union = union;
    }

    public static implicit operator UpdateEventResult(EventDetails eventDetails)
    {
        return new UpdateEventResult(eventDetails);
    }

    public static implicit operator UpdateEventResult(NotFound notFound)
    {
        return new UpdateEventResult(notFound);
    }

    public static implicit operator UpdateEventResult(DuplicateTitle duplicateTitle)
    {
        return new UpdateEventResult(duplicateTitle);
    }

    public static implicit operator UpdateEventResult(InvalidDateRange invalidDateRange)
    {
        return new UpdateEventResult(invalidDateRange);
    }
}