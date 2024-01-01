using Microsoft.AspNetCore.Mvc;
using Sample.Api.Common.Accessors;

namespace Sample.Api.Public.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IContextAccessor contextAccessor) : ControllerBase
{
    private readonly IContextAccessor _contextAccessor = contextAccessor;

    [HttpPost("SignUp")]
    public async Task<ActionResult> SignUp() => throw new NotImplementedException();

    [HttpPost("SignIn")]
    public async Task<ActionResult> SignIn() => throw new NotImplementedException();

    [HttpPost("Refresh")]
    public async Task<ActionResult> Refresh() => throw new NotImplementedException();

    [HttpPost("SignOut")]
    public async Task<ActionResult> SignOut() => throw new NotImplementedException();
}
