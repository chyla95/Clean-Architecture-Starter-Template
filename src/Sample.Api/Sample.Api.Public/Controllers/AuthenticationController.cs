using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Authentication.Jwt.Services;

namespace Sample.Api.Public.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IJwtValidatorService jwtValidatorService, IJwtGeneratorService jwtGeneratorService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<bool>> Get(CancellationToken cancellationToken)
    {
        var token = await jwtGeneratorService.GenerateTokenAsync("123", cancellationToken);
        var isTokenValid = await jwtValidatorService.IsTokenValidAsync(token, cancellationToken);
        var canTokenBeRefreshed = await jwtValidatorService.CanTokenBeRefreshedAsync(token, cancellationToken);

        return Ok(token);
    }

    [Authorize]
    [HttpGet("NeedAuth")]
    public ActionResult<bool> Get2()
    {
        return Ok(true);
    }
}
