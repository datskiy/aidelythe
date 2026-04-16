using Aidelythe.Application._Common.Sorting;

namespace Aidelythe.Application._Common.Querying;

/// <summary>
/// Represents a base query for retrieving a list of items.
/// </summary>
public abstract class GetListQuery
{
    /// <summary>
    /// Gets the unique identifier of the user requesting the list of items.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Gets the starting index of the current page in the collection of items.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// Get the number of items to return in the current page.
    /// </summary>
    public int Limit { get; }

    /// <summary>
    /// Gets the text used to filter the results of the query.
    /// </summary>
    public string? SearchText { get; }

    /// <summary>
    /// Gets the sorting scheme that defines the order in which the items should be sorted.
    /// </summary>
    public IReadOnlyCollection<SortFieldQuery>? SortFieldQueries { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetListQuery"/> class.
    /// </summary>
    /// <param name="userId">The unique identifier of the user requesting the list of items.</param>
    /// <param name="offset">The starting index of the current page in the collection of items.</param>
    /// <param name="limit">The number of items to return in the current page.</param>
    /// <param name="searchText">Optional text used to filter the results of the query.</param>
    /// <param name="sortFieldQueries">Optional sort field queries that define the order of the items.</param>
    protected GetListQuery(
        Guid userId,
        int offset,
        int limit,
        string? searchText = null,
        IReadOnlyCollection<SortFieldQuery>? sortFieldQueries = null)
    {
        UserId = userId;
        Offset = offset;
        Limit = limit;
        SearchText = searchText;
        SortFieldQueries = sortFieldQueries;
    }
}