namespace Aidelythe.Application._Common.Paging;

/// <summary>
/// Represents a paging scheme.
/// </summary>
public sealed class PagingScheme
{
    /// <summary>
    /// The minimum acceptable value of the offset.
    /// </summary>
    public const int MinimumOffsetValue = 0;

    /// <summary>
    /// The minimum acceptable value of the limit.
    /// </summary>
    public const int MinimumLimitValue = 1;

    /// <summary>
    /// The maximum acceptable value of the limit.
    /// </summary>
    public const int MaximumLimitValue = 50;

    /// <summary>
    /// Gets the offset of the page.
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// Gets the limit of the page.
    /// </summary>
    public int Limit { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PagingScheme"/> class.
    /// </summary>
    /// <param name="offset">The offset of the page.</param>
    /// <param name="limit">The limit of the page.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="offset"/> is less than <see cref="MinimumOffsetValue"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="limit"/> is less than <see cref="MinimumLimitValue"/> or
    /// greater than <see cref="MaximumLimitValue"/>.
    /// </exception>
    public PagingScheme(int offset, int limit)
    {
        ThrowIfLessThan(offset, MinimumOffsetValue);
        ThrowIfLessThan(limit, MinimumLimitValue);
        ThrowIfGreaterThan(limit, MaximumLimitValue);

        Offset = offset;
        Limit = limit;
    }
}