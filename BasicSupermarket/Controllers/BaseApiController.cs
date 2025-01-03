using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Controllers;

[Route("/api/[controller]")]
[Produces("application/json")]
[ApiController]
public class BaseApiController : ControllerBase
{
}