using Aidelythe.Api._Common.Http.Controllers;

namespace Aidelythe.Api._System.Monitoring;

/// <summary>
/// Represents a ping controller to verify the application is responsive.
/// </summary>
[Route("[controller]")]
public sealed class PingController : BaseApiController // TODO: add AllowAnonymous to the base controller and inherit it here
{
    // TODO: learn to throttle
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