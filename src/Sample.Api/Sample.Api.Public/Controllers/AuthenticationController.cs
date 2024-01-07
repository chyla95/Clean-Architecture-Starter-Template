using Microsoft.AspNetCore.Mvc;
using Sample.Api.Common.Accessors;

namespace Sample.Api.Public.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IContextAccessor contextAccessor) : ControllerBase
{
    private readonly IContextAccessor _contextAccessor = contextAccessor;

    [HttpPost("SignUp")]
    public Task<ActionResult> SignUpUser() => throw new NotImplementedException();

    [HttpPost("SignIn")]
    public Task<ActionResult> SignInUser() => throw new NotImplementedException();

    [HttpPost("Refresh")]
    public Task<ActionResult> RefreshUser() => throw new NotImplementedException();

    [HttpPost("SignOut")]
    public Task<ActionResult> SignOutUser() => throw new NotImplementedException();
}
