using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class Customer
{
    public int CustId { get; set; }

    public string? Name { get; set; }

    public long? Number { get; set; }

    public string? Email { get; set; }

    public DateTime? DateOfQuotation { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Quotation> Quotations { get; } = new List<Quotation>();
}
