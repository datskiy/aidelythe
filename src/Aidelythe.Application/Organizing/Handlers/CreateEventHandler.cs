using Aidelythe.Application.Organizing.Commands;
using Aidelythe.Application.Organizing.Results;
using Aidelythe.Domain.Organizing.ValueObjects;

namespace Aidelythe.Application.Organizing.Handlers;

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
    /// The result of the event creation.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<CreateEventResult> Handle(
        CreateEventCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: implement
        // TODO: ask and add distributed locking

        var createdEventId = new EventId(Guid.CreateVersion7());
        return await Task.FromResult(createdEventId);
    }
}