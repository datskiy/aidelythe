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

    // TODO: enforce rules

    /// <summary>
    /// Gets the search text used to filter items by matching relevant fields.
    /// </summary>
    public string SearchText { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FilteringScheme"/> class.
    /// </summary>
    /// <param name="searchText">A search text used to filter items by matching relevant fields.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="searchText"/> is null, empty, or whitespace.
    /// </exception>
    public FilteringScheme(string searchText)
    {
        if(string.IsNullOrWhiteSpace(searchText))
            throw new ArgumentException("The search text cannot be null, empty, or whitespace.", nameof(searchText));

        SearchText = searchText;
    }
}