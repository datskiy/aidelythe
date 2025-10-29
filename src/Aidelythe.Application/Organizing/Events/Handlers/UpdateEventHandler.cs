using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application.Organizing.Events.Commands;
using Aidelythe.Application.Organizing.Events.Results;

namespace Aidelythe.Application.Organizing.Events.Handlers;

/// <summary>
/// Represents a command handler for updating an event.
/// </summary>
public sealed class UpdateEventHandler : IRequestHandler<UpdateEventCommand, UpdateEventResult>
{
    /// <summary>
    /// Handles the given <see cref="UpdateEventResult"/>.
    /// </summary>
    /// <param name="request">The command to update an event.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The result of the event update.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<UpdateEventResult> Handle(
        UpdateEventCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: implement
        // TODO: ask and add distributed locking

        // var updatedEvent = new EventDetails(
        //     Guid.CreateVersion7(),
        //     "Updated title",
        //     "Updated description");
        //
        //return await Task.FromResult(updatedEvent); // TODO: revert

        return await Task.FromResult(new DuplicateTitle());
    }
}