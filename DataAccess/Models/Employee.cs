using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string EmpName { get; set; } = null!;

    public decimal? EmpSalary { get; set; }

    public string? EmpAddress { get; set; }

    public int? DeptId { get; set; }

    public virtual Department? Dept { get; set; }
}
