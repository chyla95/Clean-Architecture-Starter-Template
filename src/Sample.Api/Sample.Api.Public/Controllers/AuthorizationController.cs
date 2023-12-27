using Microsoft.AspNetCore.Mvc;

namespace Sample.Api.Public.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorizationController() : ControllerBase
{
    [HttpGet]
    public ActionResult<bool> Get()
    {
        return Ok(true);
    }
}
