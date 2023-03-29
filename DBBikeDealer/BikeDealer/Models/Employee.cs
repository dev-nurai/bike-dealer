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

    public virtual ICollection<Order> OrderEmps { get; } = new List<Order>();

    public virtual ICollection<Order> OrderUpdatedByNavigations { get; } = new List<Order>();

    public virtual ICollection<Quotation> QuotationEmps { get; } = new List<Quotation>();

    public virtual ICollection<Quotation> QuotationUpdatedByNavigations { get; } = new List<Quotation>();
}
