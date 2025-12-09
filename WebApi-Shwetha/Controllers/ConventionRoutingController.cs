using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Shwetha.Controllers
{
    
    public class ConventionRoutingController : ControllerBase
    {
        public IActionResult GetAll()
        {
            return Ok(new string[] { "Alice", "Bob", "Charlie" });
        }

        public IActionResult GetById(int id)
        {
            return Ok($"Employee ID: {id}");
        }
    }
}
