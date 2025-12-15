using Aidelythe.Api._Common.Sorting;
using Aidelythe.Application._Common.Filtering;
using Aidelythe.Application._Common.Paging;

namespace Aidelythe.Api._Common.Querying;

/// <summary>
/// Represents base parameters used for list queries.
/// </summary>
public abstract class ListQueryParams
{
    private const int DefaultOffset = 0;
    private const int DefaultLimit = 50;

    private readonly string? _searchText;
    private readonly string? _sortBy;

    /// <summary>
    /// Gets the number of entries to skip before returning the first entry.
    /// Defaults to 0 if not explicitly set.
    /// </summary>
    [FromQuery(Name = "offset")]
    [Range(PagingScheme.MinimumOffsetValue, int.MaxValue)]
    public int Offset { get; init; } = DefaultOffset;

    /// <summary>
    /// Gets the maximum number of entries to return.
    /// Defaults to 50 if not explicitly set.
    /// </summary>
    [FromQuery(Name = "limit")]
    [Range(PagingScheme.MinimumLimitValue, PagingScheme.MaximumLimitValue)]
    public int Limit { get; init; } = DefaultLimit;

    /// <summary>
    /// Gets the text used to filter entries by a specific field.
    /// Allows partial matching and is case-insensitive.
    /// No filtering is applied if not specified.
    /// </summary>
    [FromQuery(Name = "search")]
    [MaxLength(FilteringScheme.MaximumSearchTextLength)]
    public string? SearchText
    {
        get => _searchText;
        init => _searchText = NormalizeSearchText(value);
    }

    /// <summary>
    /// Gets the sorting criteria by which the entries should be sorted.
    /// The format allows multiple comma-separated pairs of field names and sorting orders.
    /// Each pair consists of a field name followed by a colon
    /// and the sort order ('asc' for ascending or 'desc' for descending).
    /// All field names and sort order values are case-insensitive.
    /// </summary>
    /// <example>
    /// fieldName1:asc,fieldName2:desc
    /// </example>
    [FromQuery(Name = "sortBy")]
    [MaxLength(SortByPolicies.MaximumLength)]
    [RegularExpression(SortByPolicies.FormatPattern)]
    public string? SortBy
    {
        get => _sortBy;
        init => _sortBy = NormalizeSortBy(value);
    }

    private static string? NormalizeSearchText(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
    
    private static string? NormalizeSortBy(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value
                .Trim()
                .Replace(" ", "");
    }
}