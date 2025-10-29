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
    /// Gets the title of the event.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the description of the event.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateEventCommand"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="title">The title of the event.</param>
    /// <param name="description">The description of the event.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="title"/> is null.</exception>
    public UpdateEventCommand(
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