using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi_Shwetha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenGenerationController : ControllerBase
    {
        // Static JWT configuration
        private static readonly string SecretKey = "mysupersecretkeyyyyyyyyyyyyyyyyyyy1234567890";
        private static readonly string Issuer = "https://localhost:7096";
        private static readonly string Audience = "https://localhost:7096";

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                if (model.Username == "admin" && model.Password == "admin123")
                {
                    string token = GenerateJwtToken(model.Username, "Admin");
                    return Ok(new { Token = token });
                }
                return Unauthorized("Invalid username or password");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Token generation failed: {ex.Message}");
            }
        }

        private string GenerateJwtToken(string username, string role)
        {
            var key = Encoding.UTF8.GetBytes(SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim("CustomClaim", "JwtFromController")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

   
}