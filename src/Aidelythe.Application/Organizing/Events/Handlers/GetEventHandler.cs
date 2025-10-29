using Aidelythe.Application.Organizing.Events.Projections;
using Aidelythe.Application.Organizing.Events.Queries;

namespace Aidelythe.Application.Organizing.Events.Handlers;

/// <summary>
/// Represents a query handler for retrieving details of a specific event.
/// </summary>
public sealed class GetEventHandler : IRequestHandler<GetEventQuery, EventDetails?>
{
    /// <summary>
    /// Handles the given <see cref="GetEventQuery"/>.
    /// </summary>
    /// <param name="request">The query to retrieve details of a specific event.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The details of the specified event.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<EventDetails?> Handle(
        GetEventQuery request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: implement

        var eventDetails = new EventDetails(
            Guid.CreateVersion7(),
            "Test title",
            "Test description");

        return await Task.FromResult(eventDetails);
    }
}