namespace Aidelythe.Api._Common.Sorting;

/// <summary>
/// Represents a base sorting provider that defines sortable fields and their mappings.
/// </summary>
public abstract class SortingProvider
{
    /// <summary>
    /// Gets a dictionary containing sortable field keys mapped to their corresponding property names.
    /// </summary>
    public IReadOnlyDictionary<string, string> SortableFieldsDictionary { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SortingProvider"/> class.
    /// </summary>
    /// <param name="sortableFieldsDictionary">
    /// A dictionary containing sortable field keys mapped to their corresponding property names.
    /// </param>
    /// <exception cref="ArgumentNullException">The <paramref name="sortableFieldsDictionary"/> is null.</exception>
    protected SortingProvider(IReadOnlyDictionary<string, string> sortableFieldsDictionary)
    {
        ThrowIfNull(sortableFieldsDictionary);

        SortableFieldsDictionary = sortableFieldsDictionary;
    }
}