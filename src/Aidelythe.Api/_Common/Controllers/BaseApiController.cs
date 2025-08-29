using Aidelythe.Api._Common.Controllers.Http;
using Aidelythe.Application._Common.Wrappers;

namespace Aidelythe.Api._Common.Controllers;

// TODO: desc for all + add the meaning of a controller without authorization
// TODO: split into 3 classes: base, public, authenticated
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class BaseApiController : ControllerBase
{
    protected const string ResourceLocator = "GetResourceById";

    /// <summary>
    /// TODO: desc + checks
    /// </summary>
    protected IActionResult PagedOk<T>( PagedResult<T> pagedResult)
    {
        Response.Headers.Append("Link", BuildLinkHeaderValue(pagedResult));
        Response.Headers.Append("X-Offset", $"{pagedResult.Offset}");
        Response.Headers.Append("X-Limit", $"{pagedResult.Limit}");
        Response.Headers.Append("X-Total-Count", $"{pagedResult.TotalCount}");

        return Ok(pagedResult.Items);
    }

    /// <summary>
    /// TODO: desc + checks
    /// </summary>
    protected IActionResult Created(Guid resourceId) // TODO: use value object Id?
    {
        return CreatedAtRoute(
            nameof(ResourceLocator),
            routeValues: new { id = resourceId },
            value: new CreatedResourceResponse { Id = resourceId });
    }

    private string BuildLinkHeaderValue<T>(PagedResult<T> pagedResult)
    {
        var linkGenerator = HttpContext.RequestServices.GetRequiredService<LinkGenerator>();

        return new LinkHeaderBuilder(HttpContext, linkGenerator)
            .Add(CreateRouteValues(pagedResult.Offset), LinkRelations.Self)
            .Add(CreateRouteValues(offset: 0), LinkRelations.First)
            .Add(CreateRouteValues(LastOffset()), LinkRelations.Last)
            .AddIf(pagedResult.HasPreviousPage, CreateRouteValues(PrevOffset()), LinkRelations.Prev)
            .AddIf(pagedResult.HasNextPage, CreateRouteValues(NextOffset()), LinkRelations.Next)
            .Build();

        int NextOffset() => pagedResult.Offset + pagedResult.Limit;
        int PrevOffset() => Math.Max(0, pagedResult.Offset - pagedResult.Limit);
        int LastOffset() => Math.Max(0, pagedResult.TotalCount - pagedResult.Limit);
        object CreateRouteValues(int offset) => new { offset, limit = pagedResult.Limit };
    }
}