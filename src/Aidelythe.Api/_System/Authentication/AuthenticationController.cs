using Aidelythe.Api._Common.Http.Controllers;
using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Api._System.Authentication.Mappers;
using Aidelythe.Api._System.Authentication.Requests;
using Aidelythe.Api.Identity;
using Aidelythe.Shared.DiscriminatedUnion;

namespace Aidelythe.Api._System.Authentication;

/// <summary>
/// Represents a controller for managing authentication.
/// </summary>
[Route("auth")]
public sealed class AuthenticationController : AnonymousApiController
{
    private readonly IMediator _mediator;

    protected override Func<IDiscriminant, string> ProblemDetailsMapper =>
        discriminant => discriminant.ToProblemDetails();

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    /// <param name="mediator">The instance of <see cref="IMediator"/>.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="mediator"/> is null.</exception>
    public AuthenticationController(IMediator mediator)
    {
        ThrowIfNull(mediator);

        _mediator = mediator;
    }

    /// <summary>
    /// Registers a user.
    /// </summary>
    /// <param name="request">The registration request containing user credentials.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the unique identifier of the registered user.
    /// May produce error responses.
    /// </returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(CreatedResourceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(UnprocessableEntityResponse), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            userId => CreatedAt<UserController>(userId),
            alreadyExists => Conflict(alreadyExists),
            missingContactMethod => UnprocessableEntity(missingContactMethod));
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request">The logging in request containing user credentials.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the authentication token pair.
    /// May produce error responses.
    /// </returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(CreatedResourceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            tokenPair => Ok(tokenPair.ToResponse()),
            invalidCredentials => Unauthorized());
    }
}