using Aidelythe.Application._Common.Paging;
using Aidelythe.Application._Common.Queries;
using Aidelythe.Application._Common.Sorting;
using Aidelythe.Application.Organizing.Events.Projections;

namespace Aidelythe.Application.Organizing.Events.Queries;

/// <summary>
/// Represents a query to retrieve a paginated list of events.
/// </summary>
public sealed class GetEventsQuery : GetListQuery, IRequest<PagedCollection<EventSummary>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetEventsQuery"/> class.
    /// </summary>
    /// <param name="offset">The starting index of the current page in the collection of items.</param>
    /// <param name="limit">The number of items to return in the current page.</param>
    /// <param name="searchText">Optional text used to filter the results of the query.</param>
    /// <param name="sortingScheme">Optional sorting scheme that defines the order of the items.</param>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="offset"/> is negative.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="limit"/> is less than 1.</exception>
    public GetEventsQuery(
        int offset,
        int limit,
        string? searchText = null,
        SortingScheme? sortingScheme = null) : base(
            offset,
            limit,
            searchText,
            sortingScheme)
    {
    }
}