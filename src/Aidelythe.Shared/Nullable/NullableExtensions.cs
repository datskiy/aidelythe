namespace Aidelythe.Shared.Nullable;

/// <summary>
/// Provides extension methods for nullable types.
/// </summary>
public static class NullableExtensions
{
    /// <summary>
    /// Maps the specified nullable object to a new value if it is not null.
    /// Otherwise, returns null.
    /// </summary>
    /// <param name="source">The nullable object to map.</param>
    /// <param name="mapper">The mapping function used to transform the source object.</param>
    /// <typeparam name="TSource">The type of the source object.</typeparam>
    /// <typeparam name="TResult">The type of the result object.</typeparam>
    /// <returns>
    /// The mapped source object if it is not null; otherwise, null.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="mapper"/> is null.</exception>
    public static TResult? IfNotNull<TSource, TResult>(
        this TSource? source,
        Func<TSource, TResult> mapper)
        where TSource : class
        where TResult : class
    {
        ThrowIfNull(mapper);

        return source is null
            ? null
            : mapper(source);
    }
}