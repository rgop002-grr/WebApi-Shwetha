using DataAccess;
using DataAccess.Models;
using DataTransferObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
           var employees= _employeeRepository.GetAll();

            return employees.Select(e => new EmployeeDto
            {
                EmpId = e.EmpId,
                EmpName = e.EmpName,
                EmpSalary =e.EmpSalary,
                EmpAddress =e.EmpAddress,
                DeptId = e.DeptId
            }).ToList();

        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public void AddEmployee(EmployeeDto emp)
        {
            if (emp.EmpSalary < 10000)
                throw new System.Exception("Salary must be at least 10,000");

            var employee = new Employee
            {
                EmpName = emp.EmpName,
                EmpSalary = emp.EmpSalary,
                EmpAddress=emp.EmpAddress,
                DeptId = emp.DeptId
            };

            _employeeRepository.Add(employee);
        }
       
        public void DeleteEmployee(int id)
        {
            _employeeRepository.Delete(id);
        }

        
    }
}
