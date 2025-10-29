using Aidelythe.Application.Organizing.Results;

namespace Aidelythe.Application.Organizing.Commands;

/// <summary>
/// Represents a command to create an event.
/// </summary>
public sealed class CreateEventCommand : IRequest<CreateEventResult>
{
    /// <summary>
    /// Gets the title of the event.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the description of the event.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEventCommand"/> class.
    /// </summary>
    /// <param name="title">The title of the event.</param>
    /// <param name="description">The description of the event.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="title"/> is null.</exception>
    public CreateEventCommand(string title, string? description)
    {
        ThrowIfNull(title);

        Title = title;
        Description = description;
    }
}