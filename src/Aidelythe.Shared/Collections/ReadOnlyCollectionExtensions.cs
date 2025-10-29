namespace Aidelythe.Shared.Collections;

/// <summary>
/// Provides extension methods for read-only collections.
/// </summary>
public static class ReadOnlyCollectionExtensions
{
    /// <summary>
    /// Returns a non-empty wrapper for the specified read-only collection.
    /// </summary>
    /// <param name="collection">The read-only collection to wrap.</param>
    /// <typeparam name="T">The type of the elements</typeparam>
    /// <returns>
    /// A non-empty wrapper for the specified read-only collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="collection"/> is null.</exception>
    public static NonEmptyCollection<T> AsNonEmpty<T>(this IReadOnlyCollection<T> collection)
    {
        ThrowIfNull(collection);

        return new NonEmptyCollection<T>(collection);
    }
}