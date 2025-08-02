using Microsoft.AspNetCore.Mvc;

namespace Aidelythe.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class PingController : ControllerBase
{
    public PingController()
    {
    }

    [HttpGet]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}