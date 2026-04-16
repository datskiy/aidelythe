using Aidelythe.Application._Common.Discriminants;

namespace Aidelythe.Application.Organizing.Events.Results;

/// <summary>
/// Represents the result of creating an event.
/// </summary>
public sealed class CreateEventResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes.
    /// </summary>
    public OneOf<Guid, AlreadyExists, InvalidDateRange> Union { get; }

    private CreateEventResult(OneOf<Guid, AlreadyExists, InvalidDateRange> union)
    {
        Union = union;
    }

    public static implicit operator CreateEventResult(Guid eventId)
    {
        return new CreateEventResult(eventId);
    }

    public static implicit operator CreateEventResult(AlreadyExists alreadyExists)
    {
        return new CreateEventResult(alreadyExists);
    }

    public static implicit operator CreateEventResult(InvalidDateRange invalidDateRange)
    {
        return new CreateEventResult(invalidDateRange);
    }
}