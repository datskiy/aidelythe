namespace Aidelythe.Shared.Collections;

/// <summary>
/// Represents a non-empty, read-only collection of elements.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface INonEmptyCollection<out T> : IReadOnlyCollection<T>
{

}