using Aidelythe.Api._Common.Controllers;

namespace Aidelythe.Api._System.Monitoring.Controllers;

// TODO: desc for all
[Route("[controller]")] // TODO: add AllowAnonymous to the base controller and inherit it here
public sealed class PingController : BaseApiController
{
    [HttpGet]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}