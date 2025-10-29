using Aidelythe.Application.Organizing.Results;

namespace Aidelythe.Application.Organizing.Commands;

/// <summary>
/// Represents a command to delete an event.
/// </summary>
public sealed class DeleteEventCommand : IRequest<DeleteEventResult>
{
    /// <summary>
    /// Gets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteEventCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    public DeleteEventCommand(Guid id)
    {
        Id = id;
    }
}