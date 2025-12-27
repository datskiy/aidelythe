using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application.Organizing.Events.Commands;
using Aidelythe.Application.Organizing.Events.Results;

namespace Aidelythe.Application.Organizing.Events.Handlers;

/// <summary>
/// Represents a command handler for deleting an event.
/// </summary>
public sealed class DeleteEventHandler : IRequestHandler<DeleteEventCommand, DeleteEventResult>
{
    /// <summary>
    /// Handles the given <see cref="DeleteEventCommand"/>.
    /// </summary>
    /// <param name="request">The command to delete an event.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of the event deletion.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<DeleteEventResult> Handle(
        DeleteEventCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: implement
        // TODO: add event owner
        // TODO: ask and add distributed locking

        return await Task.FromResult(new Success());
    }
}