namespace Aidelythe.Application.Organizing.Projections;

/// <summary>
/// Represents the details of an event.
/// </summary>
public sealed class EventDetails
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
    /// Gets the description of the event.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventDetails"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="title">The title of the event.</param>
    /// <param name="description">The description of the event.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="title"/> is null.</exception>
    public EventDetails(
        Guid id,
        string title,
        string? description)
    {
        ThrowIfNull(title);

        Id = id;
        Title = title;
        Description = description;
    }
}