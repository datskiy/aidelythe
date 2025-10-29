using Aidelythe.Api._Common.Http.Controllers;
using Aidelythe.Api.Organizing.Mappers;
using Aidelythe.Api.Organizing.Parameters;
using Aidelythe.Api.Organizing.Requests;
using Aidelythe.Application.Organizing.Commands;
using Aidelythe.Application.Organizing.Queries;
using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Api.Organizing.Controllers;

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
    /// The details of the specified event. // TODO: check against 'response' and setup for Swagger
    /// </returns>
    [HttpGet("{id:guid}", Name = nameof(ResourceLocator))]
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
    /// A paginated list of events. // TODO: check against 'response' and setup for Swagger
    /// </returns>
    [HttpGet]
    public Task<IActionResult> GetListAsync(
        [FromQuery] EventsQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        return ValidateAndRunAsync(queryParams, async () =>
        {
            var query = queryParams.ToQuery();
            var pagedCollection = await _mediator.Send(query, cancellationToken);

            return PagedOk(
                pagedCollection,
                mapper: eventSummary => eventSummary.ToResponse());
        }, cancellationToken);
    }

    /// <summary>
    /// Creates a new event.
    /// </summary>
    /// <param name="request">The event creation request containing event attributes.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A unique identifier of the created event. // TODO: check against 'response' and setup for Swagger
    /// </returns>
    [HttpPost]
    public Task<IActionResult> CreateAsync(
        [FromBody] CreateEventRequest request,
        CancellationToken cancellationToken)
    {
        return ValidateAndRunAsync(request, async () =>
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command, cancellationToken);

            return result.Union.Match<IActionResult>(
                createdId => Created(createdId),
                duplicateTitle => Conflict(duplicateTitle),
                invalidDateRange => UnprocessableEntity(invalidDateRange));
        }, cancellationToken);
    }

    /// <summary>
    /// Updates a specific event.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="request">The event update request containing event attributes.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A unique identifier of the created event. // TODO: check against 'response' and setup for Swagger
    /// </returns>
    [HttpPut("{id:guid}")]
    public Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateEventRequest request,
        CancellationToken cancellationToken)
    {
        return ValidateAndRunAsync(request, async () =>
        {
            var command = request.ToCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            return result.Union.Match<IActionResult>(
                updatedDetails => Ok(updatedDetails.ToResponse()),
                notFound => NotFound(),
                duplicateTitle => Conflict(duplicateTitle),
                invalidDateRange => UnprocessableEntity(invalidDateRange));
        }, cancellationToken);
    }

    /// <summary>
    /// Deletes a specific event.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A response indicating whether the event was successfully deleted. // TODO: check against 'response' and setup for Swagger
    /// </returns>
    [HttpDelete("{id:guid}")]
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
// check all possbile ThrowIfNegative and similar check cases
// review all code
// try passing GPT's code review
// create PR and pass your own review
// next part: commit as: ADL-13: Finalize MVP data model

// === The Event model should contain:
// Id (only for responses)
// UserId (will be done as part of authorization)
// Title
// Description (optional)
// StartsAt
// EndsAt
// CreatedAt (only for responses)
// LastUpdatedAt (only for responses)

// === Roles (at least 1)
// IsArchived or Status
// The event role should contain:
// Id (only for responses)
// Title
// Description (optional)
// MaxParticipants
// CreatedAt (only for responses) [internal]
// LastUpdatedAt (only for responses) [internal]

// [SEPARATE TASK] Add Swagger
// [SEPARATE TASK] Add mocked logging - test, see where it stores, make global error logging
// [SEPARATE TASK] #WHEN EVERYTHING IS DONE# Add tests (ask GPT should I and about the best practices)
// [LATER] Ask more about internal, mb you should revert. The problem is testing...
// [LATER] deal with appsettings.json versions, should I use explicit types like Dev or Testing; think of moving to a separate folder