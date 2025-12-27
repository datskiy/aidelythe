using Aidelythe.Application.Organizing.Events.Commands;
using Aidelythe.Application.Organizing.Events.Results;

namespace Aidelythe.Application.Organizing.Events.Handlers;

/// <summary>
/// Represents a command handler for creating an event.
/// </summary>
public sealed class CreateEventHandler : IRequestHandler<CreateEventCommand, CreateEventResult>
{
    /// <summary>
    /// Handles the given <see cref="CreateEventCommand"/>.
    /// </summary>
    /// <param name="request">The command to create an event.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the result of the event creation.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<CreateEventResult> Handle(
        CreateEventCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: implement
        // TODO: add event owner
        // TODO: ask and add distributed locking

        return await Task.FromResult(Guid.CreateVersion7());
    }
}