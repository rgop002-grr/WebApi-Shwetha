using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Shwetha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConventionRoutingController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public ConventionRoutingController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
       public IActionResult GetAll()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
       public IActionResult GetById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }
    }
}

