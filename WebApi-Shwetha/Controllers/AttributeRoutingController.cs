using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataTransferObject;
using Microsoft.IdentityModel.Tokens;

namespace WebApi_Shwetha.Controllers
{
    [Route("Swetha")]
    [ApiController]
    public class AttributeRoutingController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public AttributeRoutingController(EmployeeService employeeService)
        {
           _employeeService=employeeService;
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetAll()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(EmployeeDto emp)
        {
            _employeeService.AddEmployee(emp);
            return Ok("Employee added successfully!");
        }



        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return Ok("Employee deleted successfully!");
        }
    }
}


        
   