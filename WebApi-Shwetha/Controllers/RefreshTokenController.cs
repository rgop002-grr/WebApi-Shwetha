using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;

namespace WebApi_Shwetha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
            private readonly IConfiguration _configuration;
            public RefreshTokenController(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            // Static JWT configuration
            private static readonly string SecretKey = "mysupersecretkeyyyyyyyyyyyyyyyyyyy1234567890";
            private static readonly string Issuer = "https://localhost:7096";
            private static readonly string Audience = "https://localhost:7096";

            private static List<RefreshToken> refreshTokens = new List<RefreshToken>();

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginModel model)
            {
                if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Role))
                    return BadRequest("Username and Role are required");

                var accessToken = GenerateAccessToken(model.Username, model.Role);

                var refreshToken = GenerateRefreshToken(model.Username, model.Role);
                refreshTokens.Add(refreshToken);

                return Ok(new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token
                });
            }
            [HttpPost("refresh")]
            public IActionResult Refresh([FromBody] string token)
            {
                var storedToken = refreshTokens.FirstOrDefault(t => t.Token == token);

                if (storedToken == null || !storedToken.IsActive)
                    return Unauthorized("Invalid or expired refresh token");
                   

            // Only new access token
            var newAccessToken = GenerateAccessToken(storedToken.Username, storedToken.Role);
           

            return Ok(new
                {
                    AccessToken = newAccessToken,
                   
            });
            }
            private string GenerateAccessToken(string username, string role)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpiryMinutes"]));
                var claims = new[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),

            };

                var token = new JwtSecurityToken(
                   issuer: Issuer,
                   audience: Audience,
                   claims: claims,
                   expires: Expires,
                   signingCredentials: credentials
               );


                return new JwtSecurityTokenHandler().WriteToken(token);

            }

        private RefreshToken GenerateRefreshToken(string username, string role)
        {
            
             var expiryDays = Convert.ToInt32(_configuration["Jwt:RefreshTokenExpiryDays"]);
             var expiryDate = DateTime.UtcNow.AddMinutes(expiryDays);
              var result= new RefreshToken
             {
                 Token = Guid.NewGuid().ToString(),
                 Username = username,
                 Role = role,
                 Created = DateTime.UtcNow,
                 Expires = DateTime.UtcNow.AddDays(expiryDays)
             };
            return result;
             }

    }
}
