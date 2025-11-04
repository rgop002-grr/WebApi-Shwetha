using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public int? DeptId { get; set; }
        public string EmpName { get; set; } = null!;
        public decimal? EmpSalary { get; set; } 
        public string? EmpAddress { get; set; }
    }
}
