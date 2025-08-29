namespace Aidelythe.Api._Common.Parameters;

/// <summary>
/// TODO: desc all (Swagger style?)
/// </summary>
public sealed class ListQueryParams
{
    private const int DefaultOffset = 0;
    private const int DefaultLimit = 50;
    private const int MinLimit = 1;
    private const int MaxLimit = 100;

    private readonly int _offset = DefaultOffset;
    private readonly int _limit = DefaultLimit;
    private readonly string? _searchText;
    private readonly string? _sortBy;

    [FromQuery(Name = "offset")]
    public int Offset
    {
        get => _offset;
        init => _offset = NormalizeOffset(value);
    }

    [FromQuery(Name = "limit")]
    public int Limit
    {
        get => _limit;
        init => _limit = NormalizeLimit(value);
    }

    [FromQuery(Name = "search")] // TODO: add max length validation
    public string? SearchText
    {
        get => _searchText;
        init => _searchText = NormalizeSearchText(value);
    }

    [FromQuery(Name = "sortBy")] // TODO: implement when domain model is ready
    public string? SortBy
    {
        get => _sortBy;
        init => _sortBy = NormalizeSortBy(value);
    }

    private static int NormalizeOffset(int value)
    {
        return value < 0
            ? DefaultOffset
            : value;
    }

    private static int NormalizeLimit(int value)
    {
        return value <= 0
            ? DefaultLimit
            : Math.Clamp(value, MinLimit, MaxLimit);
    }

    private static string? NormalizeSearchText(string? value) // TODO: do the same as above -> merge?
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }

    private static string? NormalizeSortBy(string? value)
    {
        return string.IsNullOrWhiteSpace(value)
            ? null
            : value.Trim();
    }
}