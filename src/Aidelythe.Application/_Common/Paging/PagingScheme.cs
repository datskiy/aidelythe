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

    // TODO: enforce rules

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
    public PagingScheme(int offset, int limit)
    {
        Offset = offset;
        Limit = limit;
    }
}