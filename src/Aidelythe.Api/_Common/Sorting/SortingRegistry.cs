namespace Aidelythe.Api._Common.Sorting;

/// <summary>
/// Represents a registry for managing sorting configurations.
/// </summary>
/// <typeparam name="TProvider">
/// The type of the sorting provider that defines sortable fields and their mappings.
/// </typeparam>
public static class SortingRegistry<TProvider>
    where TProvider : SortingProvider, new()
{
    /// <summary>
    /// Gets a dictionary containing sortable field keys mapped to their corresponding property names
    /// for a given sorting provider type.
    /// </summary>
    public static IReadOnlyDictionary<string, string> SortableFieldsDictionary { get; }
        = new TProvider().SortableFieldsDictionary;
}