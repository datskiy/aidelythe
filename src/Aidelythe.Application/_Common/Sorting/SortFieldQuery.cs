namespace Aidelythe.Application._Common.Sorting;

/// <summary>
/// Represents a query to sort a field.
/// </summary>
/// <param name="PropertyName">The name of the property to sort by.</param>
/// <param name="IsDescending">A boolean indicating whether the sorting is descending.</param>
public readonly record struct SortFieldQuery(string PropertyName, bool IsDescending);