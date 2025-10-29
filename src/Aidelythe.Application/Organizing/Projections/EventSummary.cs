namespace Aidelythe.Application.Organizing.Projections;

/// <summary>
/// Represents the summary of an event.
/// </summary>
public sealed class EventSummary
{
    /// <summary>
    /// Gets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the title of the event.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventSummary"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="title">The title of the event.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="title"/> is null.</exception>
    public EventSummary(
        Guid id,
        string title)
    {
        ThrowIfNull(title);

        Id = id;
        Title = title;
    }
}