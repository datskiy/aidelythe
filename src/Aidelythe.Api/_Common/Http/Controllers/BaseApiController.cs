using Aidelythe.Api._Common.Http.Metadata;
using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Application._Common.Paging;
using Aidelythe.Shared.Collections;
using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Api._Common.Http.Controllers;

/// <summary>
/// Represents a base controller for API endpoints.
/// </summary>
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class BaseApiController : ControllerBase // TODO: split into 3 classes: base, public, authenticated
{
    protected const string ResourceLocator = nameof(ResourceLocator);

    // TODO: move this and Conflict with UnprocessableEntity to a separate controller + refactor as part of authorization task
    /// <summary>
    /// Gets a function that maps an <see cref="IDiscriminant"/> instance to a string representation
    /// detailing the problem.
    /// </summary>
    /// <remarks>
    /// If not overridden, the property value is <c>null</c>, and attempting to use it in conjunction with
    /// methods that rely on this mapping (e.g., the <c>Conflict</c> method) will throw an
    /// <see cref="InvalidOperationException"/>.
    /// </remarks>
    protected virtual Func<IDiscriminant, string>? ProblemDetailsMapper => null;

    /// <summary>
    /// Validates the provided instance using a registered validator
    /// and executes the specified function if validation succeeds.
    /// </summary>
    /// <typeparam name="T">The type of the instance to validate, constrained to non-nullable types.</typeparam>
    /// <param name="instance">The instance to validate.</param>
    /// <param name="next">The function to execute if validation is successful.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing <c>HTTP 400 Bad Request</c> response if validation fails,
    /// or the result of the executed function.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="instance"/> or <paramref name="next"/> is null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// A validator for the specified instance type is not registered.
    /// </exception>
    protected async Task<IActionResult> ValidateAndRunAsync<T>(
        T instance,
        Func<Task<IActionResult>> next,
        CancellationToken cancellationToken)
        where T : notnull
    {
        ThrowIfNull(instance);
        ThrowIfNull(next);

        var validator = HttpContext.RequestServices.GetRequiredService<IValidator<T>>();
        if (validator is null)
            throw new InvalidOperationException($"Validator for {typeof(T).Name} is not registered.");

        var validationResult = await validator.ValidateAsync(instance, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(new BadRequestResponse(
                validationResult.Errors.AsNonEmpty(),
                HttpContext.TraceIdentifier));

        return await next();
    }

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

        return Ok(pagedCollection
            .Select(mapper)
            .ToArray());
    }

    /// <summary>
    /// Produces an <c>HTTP 201 Created</c> response with the resource location HTTP header.
    /// </summary>
    /// <param name="resourceId">The unique identifier of the newly created resource.</param>
    /// <returns>
    /// An <see cref="IActionResult"/> representing an <c>HTTP 201 Created</c> response
    /// with the resource location header and the identifier of the created resource.
    /// </returns>
    protected IActionResult Created(Guid resourceId)
    {
        return CreatedAtRoute(
            nameof(ResourceLocator),
            routeValues: new { id = resourceId },
            value: new CreatedResourceResponse { Id = resourceId });
    }

    /// <summary>
    /// Returns an <c>HTTP 409 Conflict</c> based on the provided discriminant.
    /// </summary>
    /// <typeparam name="TDiscriminant">
    /// The type of the discriminant implementing the <see cref="IDiscriminant"/> interface.
    /// </typeparam>
    /// <param name="discriminant">The discriminant that represents operation result.</param>
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
            HttpContext.TraceIdentifier,
            detail: ProblemDetailsMapper!(discriminant)));
    }

    /// <summary>
    /// Returns an <c>HTTP 422 Unprocessable Entity</c> based on the provided discriminant.
    /// </summary>
    /// <typeparam name="TDiscriminant">
    /// The type of the discriminant implementing the <see cref="IDiscriminant"/> interface.
    /// </typeparam>
    /// <param name="discriminant">The discriminant that represents operation result.</param>
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
            HttpContext.TraceIdentifier,
            detail: ProblemDetailsMapper!(discriminant)));
    }

    private void ThrowIfProblemDetailsMapperNull()
    {
        if(ProblemDetailsMapper is null)
            throw new InvalidOperationException($"The {nameof(ProblemDetailsMapper)} is not implemented.");
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