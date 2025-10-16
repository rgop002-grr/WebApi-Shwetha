using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Shwetha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleBasedAuthController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("Admin")]
        public IActionResult Admin()
        {
            return Ok("You are Welcome,Admin can have full access.");
        }
        [Authorize(Roles = "HR")]
        [HttpGet("HR")]
        public IActionResult Hr()
        {
            return Ok("You are Welcome,Hr can access this Endpoint.");
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("Manager")]

        public IActionResult Manager()
        {
            return Ok("You are Welcome Manager,You can manage resources.");
        }

        [Authorize(Roles ="Admin,Manager")]
        [HttpGet("admin-manager")]
        public IActionResult AdminManagerEndpoint()
        {
            return Ok("Admins and Managers can access this endpoint.");
        }
    }
}
