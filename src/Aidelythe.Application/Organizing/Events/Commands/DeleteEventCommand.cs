using Aidelythe.Application.Organizing.Events.Results;

namespace Aidelythe.Application.Organizing.Events.Commands;

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
    /// Gets the unique identifier of the user deleting the event.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteEventCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="userId">The unique identifier of the user deleting the event.</param>
    public DeleteEventCommand(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}