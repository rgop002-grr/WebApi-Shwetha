using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Shwetha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        [Authorize]
        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            var username = User.Identity.Name;
            return Ok(new { Message = "Token is valid ✅", Username = username });
        }
    }
}
