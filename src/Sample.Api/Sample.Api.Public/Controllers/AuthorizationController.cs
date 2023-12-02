using Microsoft.AspNetCore.Mvc;
using Sample.Api.Common.Accessors;

namespace Sample.Api.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ISettingsAccessor _settingsAccessor;

        public AuthorizationController(ISettingsAccessor settingsAccessor)
        {
            _settingsAccessor = settingsAccessor;
        }

        [HttpGet]
        public ActionResult<bool> Get()
        {
            Console.WriteLine(_settingsAccessor.GetValue(Constants.AppSettingsKeys.JwtSecret));
            return Ok(true);
        }
    }
}
