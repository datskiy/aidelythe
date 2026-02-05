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

    /// <summary>
    /// Converts the specified task into a value task.
    /// </summary>
    /// <param name="task">The task to be converted to a value task.</param>
    /// <returns>
    /// A value task representing the same asynchronous operation as the original task.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="task"/> is null.</exception>
    public static ValueTask ToValueTask(this Task task)
    {
        ThrowIfNull(task);

        return new ValueTask(task);
    }
}