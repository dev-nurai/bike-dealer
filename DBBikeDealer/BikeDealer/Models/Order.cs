using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? CustId { get; set; }

    public int? EmpId { get; set; }

    public int? BikeModelId { get; set; }

    public long? Price { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? Status { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Remarks { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual BikeModel? BikeModel { get; set; }

    public virtual Customer? Cust { get; set; }

    public virtual Employee? Emp { get; set; }

    public virtual Status? StatusNavigation { get; set; }

    public virtual Employee? UpdatedByNavigation { get; set; }
}
