using Aidelythe.Application._Common.Locality;

namespace Aidelythe.Application.Organizing.Events.Projections;

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
    /// Gets the location of the event.
    /// </summary>
    public AddressSummary Location { get; }

    /// <summary>
    /// Gets the date and time when the event starts.
    /// </summary>
    public DateTime StartsAt { get; }

    /// <summary>
    /// Gets the date and time when the event ends.
    /// </summary>
    public DateTime? EndsAt { get; }

    /// <summary>
    /// Gets the date and time when the event was created.
    /// </summary>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// Gets the date and time when the event was last updated.
    /// </summary>
    public DateTime? LastUpdatedAt { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventSummary"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="title">The title of the event.</param>
    /// <param name="location">The location of the event.</param>
    /// <param name="startsAt">The date and time when the event starts.</param>
    /// <param name="endsAt">The date and time when the event ends.</param>
    /// <param name="createdAt">The date and time when the event was created.</param>
    /// <param name="lastUpdatedAt">The date and time when the event was last updated.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="title"/> or <paramref name="location"/> is null.
    /// </exception>
    public EventSummary(
        Guid id,
        string title,
        AddressSummary location,
        DateTime startsAt,
        DateTime? endsAt,
        DateTime createdAt,
        DateTime? lastUpdatedAt)
    {
        ThrowIfNull(title);
        ThrowIfNull(location);

        Id = id;
        Title = title;
        Location = location;
        StartsAt = startsAt;
        EndsAt = endsAt;
        CreatedAt = createdAt;
        LastUpdatedAt = lastUpdatedAt;
    }
}