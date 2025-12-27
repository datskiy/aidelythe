using Aidelythe.Api._Common.Http.Controllers;

namespace Aidelythe.Api.Identity;

/// <summary>
/// Represents a controller for managing users.
/// </summary>
[Route("users")]
public sealed class UserController : AuthorizedApiController
{
    /// <summary>
    /// Returns the details of a specific user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the details of the specified user.
    /// May produce error responses.
    /// </returns>
    [HttpGet("{id:guid}")]
    public Task<IActionResult> GetAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        // TODO: implement

        throw new NotImplementedException();
    }
}