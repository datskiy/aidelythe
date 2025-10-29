using Aidelythe.Application._Common.Sorting;

namespace Aidelythe.Application._Common.Queries;

/// <summary>
/// Represents a base query for retrieving a list of items.
/// </summary>
public abstract class GetListQuery
{
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
    public SortingScheme? SortingScheme { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetListQuery"/> class.
    /// </summary>
    /// <param name="offset">The starting index of the current page in the collection of items.</param>
    /// <param name="limit">The number of items to return in the current page.</param>
    /// <param name="searchText">Optional text used to filter the results of the query.</param>
    /// <param name="sortingScheme">Optional sorting scheme that defines the order of the items.</param>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="offset"/> is negative.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="limit"/> is less than 1.</exception>
    protected GetListQuery(
        int offset,
        int limit,
        string? searchText = null,
        SortingScheme? sortingScheme = null)
    {
        ThrowIfNegative(offset);
        ThrowIfLessThan(limit, 1);

        Offset = offset;
        Limit = limit;
        SearchText = searchText;
        SortingScheme = sortingScheme;
    }
}