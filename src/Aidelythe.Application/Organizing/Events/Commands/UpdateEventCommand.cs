using Aidelythe.Application._Common.Locality;
using Aidelythe.Application.Organizing.Events.Results;

namespace Aidelythe.Application.Organizing.Events.Commands;

/// <summary>
/// Represents a command to update an event.
/// </summary>
public sealed class UpdateEventCommand : IRequest<UpdateEventResult>
{
    /// <summary>
    /// Gets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the unique identifier of the user updating the event.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Gets the title of the event.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the description of the event.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the location of the event.
    /// </summary>
    public DefineAddressCommand Location { get; }

    /// <summary>
    /// Gets the date and time when the event starts.
    /// </summary>
    public DateTime StartsAt { get; }

    /// <summary>
    /// Gets the date and time when the event ends.
    /// </summary>
    public DateTime? EndsAt { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateEventCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="userId">The unique identifier of the user updating the event.</param>
    /// <param name="title">The title of the event.</param>
    /// <param name="description">The description of the event.</param>
    /// <param name="location">The location of the event.</param>
    /// <param name="startsAt">The date and time when the event starts.</param>
    /// <param name="endsAt">The date and time when the event ends.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="title"/> or <paramref name="location"/> is null.
    /// </exception>
    public UpdateEventCommand(
        Guid id,
        Guid userId,
        string title,
        string? description,
        DefineAddressCommand location,
        DateTime startsAt,
        DateTime? endsAt)
    {
        ThrowIfNull(title);
        ThrowIfNull(location);

        Id = id;
        UserId = userId;
        Title = title;
        Description = description;
        Location = location;
        StartsAt = startsAt;
        EndsAt = endsAt;
    }
}