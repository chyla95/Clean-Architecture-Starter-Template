using Microsoft.AspNetCore.Mvc;
using Sample.Application.WebApi.Common.Accessors;

namespace Sample.Application.WebApi.Public.Controllers;
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
