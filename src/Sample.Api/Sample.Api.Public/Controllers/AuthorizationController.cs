using Microsoft.AspNetCore.Mvc;
using Sample.Api.Common.Accessors;

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
