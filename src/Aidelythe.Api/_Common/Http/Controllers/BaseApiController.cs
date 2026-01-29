using Aidelythe.Api._Common.Http.Metadata;
using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Api._Common.Http.Routing;
using Aidelythe.Application._Common.Paging;
using Aidelythe.Shared.Unions;

namespace Aidelythe.Api._Common.Http.Controllers;

/// <summary>
/// Represents a base controller for API endpoints.
/// </summary>
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class BaseApiController : ControllerBase
{
    private const string ResourceLocator = "Get";

    /// <summary>
    /// Gets a function that maps an <see cref="IDiscriminant"/> instance to a string representation
    /// detailing the problem.
    /// </summary>
    /// <remarks>
    /// May be null if not overridden, and attempting to use it in conjunction with
    /// methods that rely on this mapping (e.g., the <c>Conflict</c> method) will throw an
    /// <see cref="InvalidOperationException"/>.
    /// </remarks>
    protected virtual Func<IDiscriminant, string>? ProblemDetailsMapper => null;

    /// <summary>
    /// Produces an <c>HTTP 200 OK</c> response with pagination-related HTTP headers.
    /// </summary>
    /// <typeparam name="T">The type of the items in the <see cref="PagedCollection{T}"/>.</typeparam>
    /// <typeparam name="TResult">The type of the items after applying the mapping function.</typeparam>
    /// <param name="pagedCollection">The paged collection of items containing pagination metadata.</param>
    /// <param name="mapper">The mapping function used to transform each item in the collection.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing an <c>HTTP 200 OK</c> response with pagination headers
    /// and a collection of transformed items.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="pagedCollection"/> or <paramref name="mapper"/> is null.
    /// </exception>
    protected IActionResult PagedOk<T, TResult>(
        PagedCollection<T> pagedCollection,
        Func<T, TResult> mapper)
        where T : notnull
    {
        ThrowIfNull(pagedCollection);
        ThrowIfNull(mapper);

        Response.Headers.Append(HttpHeaders.Link, BuildLinkHeaderValue(pagedCollection));
        Response.Headers.Append(HttpHeaders.Offset, $"{pagedCollection.Offset}");
        Response.Headers.Append(HttpHeaders.Limit, $"{pagedCollection.Limit}");
        Response.Headers.Append(HttpHeaders.TotalCount, $"{pagedCollection.TotalCount}");

        return base
            .Ok(pagedCollection
            .Select(mapper)
            .ToArray());
    }

    /// <summary>
    /// Returns an <c>HTTP 401 Unauthorized</c> response.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> representing an <c>HTTP 404 Not Found</c> response.
    /// </returns>
    protected new IActionResult Unauthorized()
    {
        return base.Unauthorized(new UnauthorizedResponse(HttpContext.TraceIdentifier));
    }

    /// <summary>
    /// Returns an <c>HTTP 404 Not Found</c> response.
    /// </summary>
    /// <returns>
    /// An <see cref="IActionResult"/> representing an <c>HTTP 404 Not Found</c> response.
    /// </returns>
    protected new IActionResult NotFound()
    {
        return base.NotFound(new NotFoundResponse(HttpContext.TraceIdentifier));
    }

    /// <summary>
    /// Produces an <c>HTTP 201 Created</c> response with the resource location HTTP header.
    /// </summary>
    /// <param name="resourceId">The unique identifier of the newly created resource.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing an <c>HTTP 201 Created</c> response
    /// with the resource location header and the identifier of the created resource.
    /// </returns>
    protected IActionResult CreatedAt<TController>(Guid resourceId)
        where TController : BaseApiController
    {
        return base.CreatedAtAction(
            actionName: ResourceLocator,
            controllerName: typeof(TController).Name.RemoveControllerSuffix(),
            routeValues: new { id = resourceId },
            value: new CreatedResourceResponse { Id = resourceId });
    }

    /// <summary>
    /// Returns an <c>HTTP 409 Conflict</c> response based on the provided discriminant.
    /// </summary>
    /// <typeparam name="TDiscriminant">
    /// The type of the discriminant implementing the <see cref="IDiscriminant"/> interface.
    /// </typeparam>
    /// <param name="discriminant">The discriminant that represents the result of an operation.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing an <c>HTTP 409 Conflict</c> response.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="discriminant"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// The <see cref="ProblemDetailsMapper"/> is not implemented.
    /// </exception>
    protected IActionResult Conflict<TDiscriminant>(TDiscriminant discriminant)
        where TDiscriminant : IDiscriminant
    {
        ThrowIfNull(discriminant);
        ThrowIfProblemDetailsMapperNull();

        return base.Conflict(new ConflictResponse(
            detail: ProblemDetailsMapper!(discriminant),
            HttpContext.TraceIdentifier));
    }

    /// <summary>
    /// Returns an <c>HTTP 422 Unprocessable Entity</c> response based on the provided discriminant.
    /// </summary>
    /// <typeparam name="TDiscriminant">
    /// The type of the discriminant implementing the <see cref="IDiscriminant"/> interface.
    /// </typeparam>
    /// <param name="discriminant">The discriminant that represents the result of an operation.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing an <c>HTTP 422 Unprocessable Entity</c> response.
    /// </returns>
    /// <exception cref="ArgumentNullException">The <paramref name="discriminant"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// The <see cref="ProblemDetailsMapper"/> is not implemented.
    /// </exception>
    protected IActionResult UnprocessableEntity<TDiscriminant>(TDiscriminant discriminant)
        where TDiscriminant : IDiscriminant
    {
        ThrowIfNull(discriminant);
        ThrowIfProblemDetailsMapperNull();

        return base.UnprocessableEntity(new UnprocessableEntityResponse(
            detail: ProblemDetailsMapper!(discriminant),
            HttpContext.TraceIdentifier));
    }

    private void ThrowIfProblemDetailsMapperNull()
    {
        if (ProblemDetailsMapper is null)
            throw new InvalidOperationException($"The {nameof(ProblemDetailsMapper)} is not overriden.");
    }

    private string BuildLinkHeaderValue<T>(PagedCollection<T> pagedCollection)
    {
        var linkGenerator = HttpContext.RequestServices.GetRequiredService<LinkGenerator>();

        return new LinkHeaderBuilder(HttpContext, linkGenerator)
            .Add(CreateRouteParams(pagedCollection.Offset), LinkRelations.Self)
            .Add(CreateRouteParams(offset: 0), LinkRelations.First)
            .Add(CreateRouteParams(LastOffset()), LinkRelations.Last)
            .AddIf(pagedCollection.HasPreviousPage, CreateRouteParams(PrevOffset()), LinkRelations.Prev)
            .AddIf(pagedCollection.HasNextPage, CreateRouteParams(NextOffset()), LinkRelations.Next)
            .Build();

        int NextOffset() => pagedCollection.Offset + pagedCollection.Limit;
        int PrevOffset() => Math.Max(0, pagedCollection.Offset - pagedCollection.Limit);
        int LastOffset() => Math.Max(0, pagedCollection.TotalCount - pagedCollection.Limit);
        LinkRouteParams CreateRouteParams(int offset) => new(offset, pagedCollection.Limit);
    }
}