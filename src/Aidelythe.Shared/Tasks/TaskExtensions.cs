namespace Aidelythe.Shared.Tasks;

/// <summary>
/// Provides extension methods for tasks.
/// </summary>
public static class TaskExtensions
{
    /// <summary>
    /// Converts the specified value into a completed task with the specified value.
    /// </summary>
    /// <typeparam name="T">The type of the value to wrap in a task.</typeparam>
    /// <param name="value">The value to wrap inside a completed task.</param>
    /// <returns>A task representing the completed task with the specified value.</returns>
    public static Task<T?> ToTask<T>(this T? value)
    {
        return Task.FromResult(value);
    }
}