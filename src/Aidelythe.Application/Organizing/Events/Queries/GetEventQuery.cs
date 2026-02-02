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
    /// Gets the unique identifier of the user requesting the event details.
    /// </summary>
    public Guid UserId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetEventQuery"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="userId">The unique identifier of the user requesting the event details.</param>
    public GetEventQuery(Guid id, Guid userId)
    {
        Id = id;
        UserId = userId;
    }
}