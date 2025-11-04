using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class DepartmentDto
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; } = null!;
        public string DeptLocation { get; set; } = null!;
    }
}
