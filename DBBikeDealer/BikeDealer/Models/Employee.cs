using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string? Name { get; set; }

    public int? Designation { get; set; }

    public int? Salary { get; set; }

    public DateTime? DateOfJoining { get; set; }

    public DateTime? DateOfResign { get; set; }

    public virtual EmployeesDesignation? DesignationNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Quotation> Quotations { get; } = new List<Quotation>();
}
