namespace Aidelythe.Application._Common.Sorting;

/// <summary>
/// Represents a sorting rule.
/// </summary>
/// <param name="PropertyName">The name of the property to sort by.</param>
/// <param name="IsDescending">A boolean indicating whether the sorting is descending.</param>
public readonly record struct SortingRule(string PropertyName, bool IsDescending);