using Aidelythe.Application._Common.Sorting;

namespace Aidelythe.Api._Common.Sorting;

/// <summary>
/// Provides utility functions for sorting.
/// </summary>
public static class SortingHelper
{
    private const string AscToken = "asc";
    private const string DescToken = "desc";

    /// <summary>
    /// Parses a formatted sorting string into a collection of <see cref="SortFieldQuery"/>.
    /// </summary>
    /// <param name="sortBy">A formatted sorting string representing sorting fields.</param>
    /// <param name="sortableFieldDictionary">
    /// A dictionary containing sortable field keys mapped to their corresponding property names.
    /// </param>
    /// <returns>
    /// A collection of <see cref="SortFieldQuery"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="sortBy"/> or <paramref name="sortableFieldDictionary"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">The <paramref name="sortableFieldDictionary"/> is empty.</exception>
    /// <exception cref="ArgumentException">The sorting direction is invalid.</exception>
    public static IReadOnlyCollection<SortFieldQuery> ParseSortingFields(
        string sortBy,
        IReadOnlyDictionary<string, string> sortableFieldDictionary)
    {
        ThrowIfNull(sortBy);
        ThrowIfNull(sortableFieldDictionary);

        if(sortableFieldDictionary.Count < 1)
            throw new ArgumentException(
                "The dictionary must contain at least one element.",
                nameof(sortableFieldDictionary));

        return sortBy
            .Split(',')
            .Select(sortingField =>
            {
                var sortingFieldParts = sortingField.Split(':');
                return new SortFieldQuery(
                    PropertyName: sortableFieldDictionary[sortingFieldParts[0]],
                    IsDescending: ParseIsDescending(sortingFieldParts[1]));
            })
            .ToArray();
    }

    private static bool ParseIsDescending(string sortingDirection)
    {
        return sortingDirection.ToLowerInvariant() switch
        {
            AscToken => false,
            DescToken => true,
            _ => throw new ArgumentException($"The sorting direction '{sortingDirection}' is invalid.")
        };
    }
}