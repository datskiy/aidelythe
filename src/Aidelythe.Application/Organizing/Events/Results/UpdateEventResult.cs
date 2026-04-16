using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application.Organizing.Events.Projections;

namespace Aidelythe.Application.Organizing.Events.Results;

/// <summary>
/// Represents the result of updating an event.
/// </summary>
public sealed class UpdateEventResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes.
    /// </summary>
    public OneOf<EventDetails, NotFound, AlreadyExists, InvalidDateRange> Union { get; }

    private UpdateEventResult(OneOf<EventDetails, NotFound, AlreadyExists, InvalidDateRange> union)
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

    public static implicit operator UpdateEventResult(AlreadyExists alreadyExists)
    {
        return new UpdateEventResult(alreadyExists);
    }

    public static implicit operator UpdateEventResult(InvalidDateRange invalidDateRange)
    {
        return new UpdateEventResult(invalidDateRange);
    }
}