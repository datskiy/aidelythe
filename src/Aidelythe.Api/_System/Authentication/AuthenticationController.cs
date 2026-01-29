using Aidelythe.Api._Common.Http.Controllers;
using Aidelythe.Api._Common.Http.Responses;
using Aidelythe.Api._System.Authentication.Mappers;
using Aidelythe.Api._System.Authentication.Requests;
using Aidelythe.Api._System.Authentication.Responses;
using Aidelythe.Api._System.Authentication.Services;
using Aidelythe.Api.Identity;
using Aidelythe.Application._System.Authentication.Commands;
using Aidelythe.Shared.Guards;
using Aidelythe.Shared.Unions;

namespace Aidelythe.Api._System.Authentication;

/// <summary>
/// Represents a controller for managing authentication.
/// </summary>
[Route("auth")]
public sealed class AuthenticationController : AnonymousApiController
{
    private readonly IMediator _mediator;
    private readonly IUserSessionContextAccessor _userSessionContextAccessor;

    protected override Func<IDiscriminant, string> ProblemDetailsMapper =>
        discriminant => discriminant.ToProblemDetails();

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
    /// </summary>
    /// <param name="mediator">The instance of <see cref="IMediator"/>.</param>
    /// <param name="userSessionContextAccessor">The instance of <see cref="IUserSessionContextAccessor"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// The <paramref name="mediator"/> or <paramref name="userSessionContextAccessor"/>  is null.
    /// </exception>
    public AuthenticationController(
        IMediator mediator,
        IUserSessionContextAccessor userSessionContextAccessor)
    {
        ThrowIfNull(mediator);
        ThrowIfNull(userSessionContextAccessor);

        _mediator = mediator;
        _userSessionContextAccessor = userSessionContextAccessor;
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
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
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
    /// The task result contains the details of the issued token pair.
    /// May produce error responses.
    /// </returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenPairDetailsResponse), StatusCodes.Status200OK)]
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

    /// <summary>
    /// Refreshes the token pair of a user.
    /// </summary>
    /// <param name="request">The refresh request containing the refresh token of a user.</param>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the details of the refreshed token pair.
    /// May produce error responses.
    /// </returns>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenPairDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await _mediator.Send(command, cancellationToken);

        return result.Union.Match<IActionResult>(
            tokenPair => Ok(tokenPair.ToResponse()),
            invalidToken => Unauthorized());
    }

    /// <summary>
    /// Logs out the current user session.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains nothing.
    /// May produce error responses.
    /// </returns>
    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var userSessionContext = _userSessionContextAccessor.UserSessionContext.ThrowIfNull();

        var command = new LogoutCommand(userSessionContext.UserSessionId);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Logs out all user sessions.
    /// </summary>
    /// <param name="cancellationToken">A token used to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains nothing.
    /// May produce error responses.
    /// </returns>
    [Authorize]
    [HttpPost("logout/all")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutAll(CancellationToken cancellationToken)
    {
        var userSessionContext = _userSessionContextAccessor.UserSessionContext.ThrowIfNull();

        var command = new LogoutAllCommand(userSessionContext.UserId);
        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }
}