using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "API is running" });
    }
}