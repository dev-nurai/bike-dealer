using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class Status
{
    public int Id { get; set; }

    public string? Status1 { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
