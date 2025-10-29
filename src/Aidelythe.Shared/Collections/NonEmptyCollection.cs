using System.Collections.ObjectModel;

namespace Aidelythe.Shared.Collections;

/// <summary>
/// Represents a non-empty wrapper around read-only collection.
/// </summary>
/// <typeparam name="T">The type of the elements.</typeparam>
public sealed class NonEmptyCollection<T> : INonEmptyCollection<T>
{
    private readonly IReadOnlyCollection<T> _collection;

    /// <inheritdoc/>
    public int Count => _collection.Count;

    /// <summary>
    /// Initializes a new instance of the <see cref="NonEmptyCollection{T}"/> class.
    /// </summary>
    /// <param name="collection">The read-only collection to wrap.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="collection"/> is null.</exception>
    /// <exception cref="ArgumentException">The <paramref name="collection"/> is empty.</exception>
    public NonEmptyCollection(IReadOnlyCollection<T> collection)
    {
        ThrowIfNull(collection);

        if(collection.Count < 1)
            throw new ArgumentException(
                "The collection must contain at least one element.",
                nameof(collection));

        _collection = collection;
    }

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        return _collection.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}