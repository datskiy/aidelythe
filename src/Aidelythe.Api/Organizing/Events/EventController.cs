using Aidelythe.Api._Common.Http.Controllers;
using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Api.Organizing.Events.Mappers;
using Aidelythe.Api.Organizing.Events.Requests;
using Aidelythe.Api.Organizing.Events.Responses;
using Aidelythe.Application.Organizing.Events.Commands;
using Aidelythe.Application.Organizing.Events.Queries;
using Aidelythe.Shared.Unions;

namespace Aidelythe.Api.Organizing.Events;

/// <summary>
/// Represents a controller for managing events.
/// </summary>
[Route("events")]
public sealed class EventController : AuthorizedApiController
{
    private readonly IMediator _mediator;

    protected override Func<IDiscriminant, string> ProblemDetailsMapper =>
        discriminant => discriminant.ToProblemDetails();

    /// <summary>
    /// Initializes a new instance of the <see cref="EventController"/> class.
    /// </summary>
    /// <param name="mediator">The instance of <see cref="IMediator"/>.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="mediator"/> is null.</exception>
    public EventController(IMediator mediator)
    {
        ThrowIfNull(mediator);

        _mediator = mediator;
    }

    /// <summary>
    /// Returns the details of a specific event.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the details of the specified event.
    /// May produce error responses.
    /// </returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(EventDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetEventQuery(id, CurrentUserId);
        var eventDetails = await _mediator.Send(query, cancellationToken);

        return eventDetails is not null
            ? Ok(eventDetails.ToResponse())
            : NotFound();
    }

    /// <summary>
    /// Returns a paginated list of events.
    /// </summary>
    /// <param name="queryParams">The query parameters for filtering, sorting, and pagination.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a paginated list of events.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(EventSummaryResponse[]), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] EventsQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        var query = queryParams.ToQuery(CurrentUserId);
        var pagedCollection = await _mediator.Send(query, cancellationToken);

        return PagedOk(
            pagedCollection,
            mapper: eventSummary => eventSummary.ToResponse());
    }

    /// <summary>
    /// Creates a new event.
    /// </summary>
    /// <param name="request">The event creation request containing event attributes.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the unique identifier of the created event.
    /// May produce error responses.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedResourceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(UnprocessableEntityResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateEventRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(CurrentUserId);
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            eventId => CreatedAt<EventController>(eventId),
            alreadyExists => Conflict(alreadyExists),
            invalidDateRange => UnprocessableEntity(invalidDateRange));
    }

    /// <summary>
    /// Updates a specific event.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="request">The event update request containing event attributes.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the details of the updated event.
    /// May produce error responses.
    /// </returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EventDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(UnprocessableEntityResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateEventRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id, CurrentUserId);
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            eventDetails => Ok(eventDetails.ToResponse()),
            notFound => NotFound(),
            alreadyExists => Conflict(alreadyExists),
            invalidDateRange => UnprocessableEntity(invalidDateRange));
    }

    /// <summary>
    /// Deletes a specific event.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains nothing.
    /// May produce error responses.
    /// </returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteEventCommand(id, CurrentUserId);
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            success => NoContent(),
            notFound => NotFound());
    }
}