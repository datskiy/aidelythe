using Aidelythe.Application._Common.Discriminants;
using Aidelythe.Application.Organizing.Commands;
using Aidelythe.Application.Organizing.Results;

namespace Aidelythe.Application.Organizing.Handlers;

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
    /// The result of the event deletion.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<DeleteEventResult> Handle(
        DeleteEventCommand request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: implement
        // TODO: ask and add distributed locking

        return await Task.FromResult(new Success());
    }
}