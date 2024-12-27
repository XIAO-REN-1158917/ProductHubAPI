using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ASP.NET.Core.WebAPI.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenDemoController : ControllerBase
    {
        [Authorize(Policy = "StaffLevel")]
        [HttpGet("staff")]
        public ActionResult<dynamic> StaffLevel()
        {
            return new List<dynamic>()
            {
                new {Msg="Hi Staff" }

            };
        }

        [Authorize(Policy = "AdminLevel")]
        [HttpGet("admin")]
        public ActionResult<dynamic> AdminLevel()
        {
            return new List<dynamic>()
            {
                new { Msg ="Hi Admin" }
            };
        }
    }
}
