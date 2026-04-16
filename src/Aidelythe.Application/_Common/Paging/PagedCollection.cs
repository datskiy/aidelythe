namespace Aidelythe.Application._Common.Paging;

/// <summary>
/// Represents a read-only collection of items that includes pagination metadata.
/// </summary>
/// <typeparam name="T">The type of items in the collection.</typeparam>
public sealed class PagedCollection<T> : IReadOnlyCollection<T>
{
    private readonly IReadOnlyCollection<T> _items;

    /// <summary>
    /// Gets the number of items in the current page of the collection.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Gets the total number of items available across all pages in the collection.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Gets the zero-based index of the first item in the current page of the collection.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// Gets the maximum number of items that can be included in a single page of the collection.
    /// </summary>
    public int Limit { get; }

    /// <summary>
    /// Indicates whether there is a previous page of items in the collection.
    /// </summary>
    public bool HasPreviousPage => Offset > 0;

    /// <summary>
    /// Indicates whether there is another page of items available after the current page.
    /// </summary>
    public bool HasNextPage => Offset + Limit < TotalCount;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedCollection{T}"/> class.
    /// </summary>
    /// <param name="items">The collection of items for the current page.</param>
    /// <param name="offset">The zero-based index of the first item in the current page.</param>
    /// <param name="limit">The maximum number of items that can be included in a single page.</param>
    /// <param name="totalCount">The total number of items available across all pages.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="items"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="offset"/> or <paramref name="totalCount"/> is negative.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">The <paramref name="limit"/> is less than 1.</exception>
    public PagedCollection(
        IReadOnlyCollection<T> items,
        int offset,
        int limit,
        int totalCount)
    {
        ThrowIfNull(items);
        ThrowIfNegative(offset);
        ThrowIfNegative(totalCount);
        ThrowIfLessThan(limit, other: 1);

        _items = items;
        Offset = offset;
        Limit = limit;
        TotalCount = totalCount;
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}