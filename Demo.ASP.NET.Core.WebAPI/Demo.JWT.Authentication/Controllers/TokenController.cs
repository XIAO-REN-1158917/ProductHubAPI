using Demo.JWT.Authentication.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.JWT.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JWTDbContext _context;
        public TokenController(JWTDbContext context)
        {
            _context = context;
        }
        private const string signingKey = "MySecretKey123456789012345678901234567890";
        private const string issuer = "Gary";
        private const string audience = "TokenDemoAPI";


        private ClaimsPrincipal GetPrincipalFromAccessToken(string token)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler();
            var claimsPrincipal = jwtSecurityToken.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                ValidateLifetime = false
            }, out SecurityToken validatedToken);
            return claimsPrincipal;
        }

        [HttpGet("Refresh")]
        public ActionResult BuildRefreshToken(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) return NotFound();
            var userClaims = GetPrincipalFromAccessToken(accessToken);
            if (userClaims == null) return NotFound();
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid,userClaims.FindFirst(u=>u.Type.Equals(ClaimTypes.Sid))!.Value),
                new Claim(ClaimTypes.Name,userClaims.FindFirst(u=>u.Type.Equals(ClaimTypes.Name))!.Value),
                new Claim(ClaimTypes.Role,userClaims.FindFirst(u=>u.Type.Equals(ClaimTypes.Role))!.Value)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(token);
        }



        [HttpGet]
        public async Task<ActionResult> BuildAccessToken(string userName, string userPwd)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userPwd))
                return NotFound();
            var user = await _context.JWTUsers!.AsNoTracking().FirstOrDefaultAsync(u => u.UserName!.Equals(userName) && u.UserPwd!.Equals(userPwd));
            if (user == null)
                return BadRequest("Invalide username or password");
            var role = user.Role!;
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid,user.UserId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role,role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(issuer, audience, claims, expires: DateTime.UtcNow.AddSeconds(5), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Ok(token);
        }

    }
}
