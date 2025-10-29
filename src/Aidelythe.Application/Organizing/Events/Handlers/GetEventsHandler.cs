using Aidelythe.Application._Common.Paging;
using Aidelythe.Application.Organizing.Events.Projections;
using Aidelythe.Application.Organizing.Events.Queries;

namespace Aidelythe.Application.Organizing.Events.Handlers;

/// <summary>
/// Represents a query handler for retrieving a paginated list of events.
/// </summary>
public sealed class GetEventsHandler : IRequestHandler<GetEventsQuery, PagedCollection<EventSummary>>
{
    /// <summary>
    /// Handles the given <see cref="GetEventsQuery"/>.
    /// </summary>
    /// <param name="request">The query to retrieve a paginated list of events.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A paginated list of events.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="request"/> is null.</exception>
    public async Task<PagedCollection<EventSummary>> Handle(
        GetEventsQuery request,
        CancellationToken cancellationToken)
    {
        ThrowIfNull(request);

        // TODO: implement

        var eventSummaries = new[]
        {
            new EventSummary(
                Guid.CreateVersion7(),
                "My title summary #1"),
            new EventSummary(
                Guid.CreateVersion7(),
                "My title summary #2"),
            new EventSummary(
                Guid.CreateVersion7(),
                "My title summary #3"),
        };

        var pagedCollection = new PagedCollection<EventSummary>(
            eventSummaries,
            request.Offset,
            request.Limit,
            totalCount: 3);

        return await Task.FromResult(pagedCollection);
    }
}