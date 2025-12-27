namespace Aidelythe.Application._Common.Sorting;

/// <summary>
/// Represents a sorting rule.
/// </summary>
public readonly record struct SortingRule
{
    /// <summary>
    /// Gets the name of the property to sort by.
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// Gets a value indicating whether the sorting is descending.
    /// </summary>
    public bool IsDescending { get; }

    // TODO: enforce rules

    /// <summary>
    /// Initializes a new instance of the <see cref="SortingRule"/> class.
    /// </summary>
    /// <param name="propertyName">The name of the property to sort by.</param>
    /// <param name="isDescending">A value indicating whether the sorting is descending.</param>
    /// <exception cref="ArgumentException">
    /// The <paramref name="propertyName"/> is null, empty, or consists only of white-space characters.
    /// </exception>
    public SortingRule(string propertyName, bool isDescending)
    {
        ThrowIfNullOrWhiteSpace(propertyName);

        PropertyName = propertyName;
        IsDescending = isDescending;
    }
}