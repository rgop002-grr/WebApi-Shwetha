using BusinessLayer;
using DataAccess.Models;
using DataTransferObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApi_Shwetha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentationLayerController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public PresentationLayerController(EmployeeService employeeService)
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

        [HttpPost] 
        public IActionResult Create(EmployeeDto emp)
        { _employeeService.AddEmployee(emp); 
            return Ok("Employee added successfully!"); 
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return Ok("Employee deleted successfully!");
        }
    }
}

