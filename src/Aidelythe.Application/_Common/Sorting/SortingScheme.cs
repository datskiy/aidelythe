using Aidelythe.Shared.Collections;

namespace Aidelythe.Application._Common.Sorting;

/// <summary>
/// Represents a sorting scheme.
/// </summary>
public sealed class SortingScheme : IReadOnlyCollection<SortingRule>
{
    private readonly IReadOnlyCollection<SortingRule> _sortingRules;

    /// <summary>
    /// Gets the number of sorting rules.
    /// </summary>
    public int Count => _sortingRules.Count;

    // TODO: enforce rules

    /// <summary>
    /// Initializes a new instance of the <see cref="SortingScheme"/> class.
    /// </summary>
    /// <param name="sortingRules">The collection of sorting rules.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="sortingRules"/> is null.</exception>
    public SortingScheme(INonEmptyCollection<SortingRule> sortingRules)
    {
        ThrowIfNull(sortingRules);

        _sortingRules = sortingRules;
    }

    /// <inheritdoc/>
    public IEnumerator<SortingRule> GetEnumerator()
    {
        return _sortingRules.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}