using Aidelythe.Application._Common.Sorting;

namespace Aidelythe.Api._Common.Sorting;

/// <summary>
/// Provides utility functions for sorting.
/// </summary>
public static class SortingHelper
{
    private const string DescToken = "desc";

    /// <summary>
    /// Parses a formatted sorting string into a collection of <see cref="SortingRule"/>.
    /// </summary>
    /// <param name="sortBy">A formatted sorting string representing sorting rules.</param>
    /// <param name="sortableFieldDictionary">
    /// A dictionary containing sortable field keys mapped to their corresponding property names.
    /// </param>
    /// <returns>
    /// A collection of <see cref="SortingRule"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="sortBy"/> or <paramref name="sortableFieldDictionary"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">The <paramref name="sortableFieldDictionary"/> is empty.</exception>
    public static IReadOnlyCollection<SortingRule> ParseRules(
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
            .Select(sortingRule =>
            {
                var sortingRuleParts = sortingRule.Split(':');
                return new SortingRule(
                    PropertyName: sortableFieldDictionary[sortingRuleParts[0]],
                    IsDescending: sortingRuleParts[1].Equals(DescToken, StringComparison.OrdinalIgnoreCase));
            })
            .ToArray();
    }
}