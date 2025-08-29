namespace Aidelythe.Application._Common.Wrappers;

/// <summary>
/// TODO: desc + checks
/// </summary>
public sealed class PagedResult<T>
{
    public IReadOnlyCollection<T> Items { get; }

    public int Offset { get; }

    public int Limit { get; }

    public int TotalCount { get; }

    public bool HasPreviousPage => Offset > 0;

    public bool HasNextPage => Offset + Limit < TotalCount;

    public PagedResult(
        IReadOnlyCollection<T> items,
        int offset,
        int limit,
        int totalCount)
    {
        Items = items;
        Offset = offset;
        Limit = limit;
        TotalCount = totalCount;
    }
}