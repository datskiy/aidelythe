using Aidelythe.Api._Common.Controllers;
using Aidelythe.Api._Common.Parameters;
using Aidelythe.Api.Organizing.Requests;
using Aidelythe.Api.Organizing.Responses;
using Aidelythe.Application._Common.Wrappers;

namespace Aidelythe.Api.Organizing.Controllers;

// TODO: desc
[Route("events")]
public sealed class EventController : BaseApiController
{
    /// <summary>
    /// TODO: desc Get
    /// </summary>
    //[HttpGet("test/{id:guid}")]
    [HttpGet("{id:guid}", Name = nameof(ResourceLocator))]
    public async Task<IActionResult> GetAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // imitating operation...

        var response = new EventResponse
        {
            Id = id,
            Title = "My title",
            Description = "My description"
        };

        return Ok(response);
    }

    /// <summary>
    /// TODO: desc GetList
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListAsync(
        [FromQuery] ListQueryParams listQueryParams,
        CancellationToken cancellationToken) // TODO: add sorting and search
    {
        await Task.Delay(1, cancellationToken); // imitating operation...
        var items = new[] // TODO: add mapping support from a different collection type
        {
            new EventSummaryResponse
            {
                Id = Guid.CreateVersion7(),
                Title = "My title1",
                Description = "My description1"
            },
            new EventSummaryResponse
            {
                Id = Guid.CreateVersion7(),
                Title = "My title2",
                Description = "My description2"
            },
            new EventSummaryResponse
            {
                Id = Guid.CreateVersion7(),
                Title = "My title3"
            }
        };

        // TODO: return summaries instead

        var pagedResult = new PagedResult<EventSummaryResponse>(
            items,
            listQueryParams.Offset,
            listQueryParams.Limit,
            totalCount: 3);

        return PagedOk(pagedResult);
    }

    /// <summary>
    /// TODO: desc Create
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateEventRequest request,
        CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // imitating operation...
        var createdId = Guid.CreateVersion7();

        return Created(createdId);
    }

    /// <summary>
    /// TODO: desc Update
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateEventRequest request,
        CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // imitating operation...
        var response = new EventResponse
        {
            Id = id,
            Title = "Updated title",
            Description = "Updated description"
        };

        return Ok(response);
    }

    /// <summary>
    /// TODO: desc Delete
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        await Task.Delay(1, cancellationToken); // imitating operation...

        return NoContent();
    }
}
// TODO: list
// Fix TODOs
// Move appsettings.json to a Configuration folder and test that it's okay
// Ask GPt about tests, do I even need them and how do I implement them for Api
// try passing GPT's code review
// create PR and pass your own review