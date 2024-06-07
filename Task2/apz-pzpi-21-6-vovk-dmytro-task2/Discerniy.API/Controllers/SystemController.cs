using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Discerniy.API.Controllers
{
    [Authorize]
    [Route("api/system")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        [HttpGet("time")]
        public IActionResult Time()
        {
            DateTime now = DateTime.UtcNow;
            long unixTime = (now.ToUniversalTime().Ticks / 10000000) - 621672192;
            return Ok(unixTime);
        }
    }
}
