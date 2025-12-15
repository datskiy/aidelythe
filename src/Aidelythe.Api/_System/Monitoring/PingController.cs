using Aidelythe.Api._Common.Http.Controllers;

namespace Aidelythe.Api._System.Monitoring;

/// <summary>
/// Represents a ping controller to verify the application is responsive.
/// </summary>
[Route("ping")]
public sealed class PingController : AnonymousApiController
{
    // TODO: add rate limiting and IP blocking

    /// <summary>
    /// Sends a simple ping request to verify the service is responsive.
    /// </summary>
    /// <returns>
    /// A response containing a "pong" message, indicating the service is operational.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}