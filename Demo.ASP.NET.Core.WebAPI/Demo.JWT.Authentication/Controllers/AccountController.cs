using Demo.JWT.Authentication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.JWT.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTDbContext _context;
        public AccountController(JWTDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> RegisterUser(JWTUser user)
        {
            if (user == null) return NotFound();
            _context.JWTUsers?.Add(user);
            var count = await _context.SaveChangesAsync();
            return count;
        }
    }
}
