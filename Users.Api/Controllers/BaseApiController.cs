using Microsoft.AspNetCore.Mvc;

namespace Users.Api.Controllers;

[Route("/api/[controller]")]
[Produces("application/json")]
[ApiController]
public class BaseApiController : ControllerBase
{

}
