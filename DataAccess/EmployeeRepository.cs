using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EmployeeRepository
    {
        private readonly SwethaDbContext _context;

        public EmployeeRepository(SwethaDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees;
        }
        public Employee GetById(int id)
        {
            return _context.Employees.FirstOrDefault(e => e.EmpId == id);
        }
        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }
       
        public void Delete(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
        }

      
    }
}
