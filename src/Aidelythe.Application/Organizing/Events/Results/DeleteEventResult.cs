using Aidelythe.Application._Common.Discriminants;

namespace Aidelythe.Application.Organizing.Events.Results;

/// <summary>
/// Represents the result of deleting an event.
/// </summary>
public sealed class DeleteEventResult
{
    /// <summary>
    /// Gets the discriminated union containing all possible outcomes when deleting an event.
    /// </summary>
    public OneOf<Success, NotFound> Union { get; }

    private DeleteEventResult(OneOf<Success, NotFound> union)
    {
        Union = union;
    }

    public static implicit operator DeleteEventResult(Success success)
    {
        return new DeleteEventResult(success);
    }

    public static implicit operator DeleteEventResult(NotFound notFound)
    {
        return new DeleteEventResult(notFound);
    }
}