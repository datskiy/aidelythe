using Aidelythe.Api._Common.Http.Controllers;
using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Api.Organizing.Events.Mappers;
using Aidelythe.Api.Organizing.Events.Requests;
using Aidelythe.Api.Organizing.Events.Responses;
using Aidelythe.Application.Organizing.Events.Commands;
using Aidelythe.Application.Organizing.Events.Queries;
using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Api.Organizing.Events;

/// <summary>
/// Represents a controller for managing events.
/// </summary>
[Route("events")]
public sealed class EventController : BaseApiController
{
    private readonly IMediator _mediator;

    protected override Func<IDiscriminant, string>? ProblemDetailsMapper =>
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
    /// The details of the specified event.
    /// </returns>
    [HttpGet("{id:guid}", Name = nameof(ResourceLocator))]
    [ProducesResponseType(typeof(EventDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new GetEventQuery(id);
        var instance = await _mediator.Send(query, cancellationToken);

        return instance is not null
            ? Ok(instance.ToResponse())
            : NotFound();
    }

    /// <summary>
    /// Returns a paginated list of events.
    /// </summary>
    /// <param name="queryParams">The query parameters for filtering, sorting, and pagination.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A paginated list of events.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(EventSummaryResponse[]), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] EventsQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        var query = queryParams.ToQuery();
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
    /// A unique identifier of the created event.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedResourceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(UnprocessableEntityResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateEventRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            createdId => Created(createdId),
            duplicateTitle => Conflict(duplicateTitle),
            invalidDateRange => UnprocessableEntity(invalidDateRange));
    }

    /// <summary>
    /// Updates a specific event.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="request">The event update request containing event attributes.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A unique identifier of the created event.
    /// </returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EventDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(UnprocessableEntityResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateEventRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            updatedDetails => Ok(updatedDetails.ToResponse()),
            notFound => NotFound(),
            duplicateTitle => Conflict(duplicateTitle),
            invalidDateRange => UnprocessableEntity(invalidDateRange));
    }

    /// <summary>
    /// Deletes a specific event.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A response indicating whether the event was successfully deleted.
    /// </returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteEventCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            success => NoContent(),
            notFound => NotFound());
    }
}

// TODO: list
// check all possible ThrowIfNegative and similar check cases
// review all code
// try passing GPT's code review
// create PR and pass your own review

// [SEPARATE TASK] #WHEN EVERYTHING IS DONE# Add tests (ask GPT should I and about the best practices)
// [LATER] Ask more about internal, mb you should revert. The problem is testing...
// [LATER] deal with appsettings.json versions, should I use explicit types like Dev or Testing; think of moving to a separate folder
// [LATER] Add health checks
// [LATER] Find examples of development ready appsettings.json (wit AllowedHosts, etc.)