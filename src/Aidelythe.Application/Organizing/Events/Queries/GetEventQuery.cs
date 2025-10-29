using Aidelythe.Application.Organizing.Events.Projections;

namespace Aidelythe.Application.Organizing.Events.Queries;

/// <summary>
/// Represents a query to retrieve details of a specific event.
/// </summary>
public sealed class GetEventQuery : IRequest<EventDetails?>
{
    /// <summary>
    /// Gets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEventQuery"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    public GetEventQuery(Guid id)
    {
        Id = id;
    }
}