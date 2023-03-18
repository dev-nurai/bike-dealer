using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class EmployeesDesignation
{
    public int EmpDesignationId { get; set; }

    public string? Designation { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
