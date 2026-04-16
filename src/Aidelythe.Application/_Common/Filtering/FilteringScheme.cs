namespace Aidelythe.Application._Common.Filtering;

/// <summary>
/// Represents a filtering scheme.
/// </summary>
public sealed class FilteringScheme
{
    /// <summary>
    /// The maximum acceptable length of the search text.
    /// </summary>
    public const int MaximumSearchTextLength = 100;

    /// <summary>
    /// Gets the search text used to filter items by matching relevant fields.
    /// </summary>
    public string SearchText { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FilteringScheme"/> class.
    /// </summary>
    /// <param name="searchText">A search text used to filter items by matching relevant fields.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="searchText"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The <paramref name="searchText"/> is longer than <see cref="MaximumSearchTextLength"/>.
    /// </exception>
    public FilteringScheme(string searchText)
    {
        ThrowIfNullOrWhiteSpace(searchText);
        ThrowIfLongerThan(searchText, MaximumSearchTextLength);

        SearchText = searchText;
    }
}