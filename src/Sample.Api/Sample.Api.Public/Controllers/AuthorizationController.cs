using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Authentication.Jwt.Services;

namespace Sample.Api.Public.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorizationController(IJwtValidatorService jwtValidatorService, IJwtGeneratorService jwtGeneratorService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<bool>> Get()
    {
        var token = await jwtGeneratorService.GenerateTokenAsync("123");
        var isTokenValid = await jwtValidatorService.IsTokenValidAsync(token);

        return Ok(token);
    }

    [Authorize]
    [HttpGet("NeedAuth")]
    public ActionResult<bool> Get2()
    {
        return Ok(true);
    }
}
