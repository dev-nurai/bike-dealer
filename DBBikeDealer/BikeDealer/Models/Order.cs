using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustId { get; set; }

    public int? EmpId { get; set; }

    public int? BikeModelId { get; set; }

    public int? OrderAccessories { get; set; }

    public long? Price { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual BikeModel? BikeModel { get; set; }

    public virtual Customer? Cust { get; set; }

    public virtual Employee? Emp { get; set; }
}
