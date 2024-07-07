using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AuthenticationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private string GenerateJWT(string username, string role, string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new[] {
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.Role,role)
            };
            var token = new JwtSecurityToken(
                issuer: "https://www.swathy.com",
                audience: "https://www.swathy.com",
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials,
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpGet("Token/{username}/{role}/{secretKey}")]
        public ActionResult GetToken(string username, string role, string secretKey)
        {
            string token = GenerateJWT(username, role, secretKey);
            return Ok(token);
        }
    }
}
