using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class BikeModel
{
    public int BikeModelId { get; set; }

    public string? ModelName { get; set; }

    public short? ModelYear { get; set; }

    public long? Price { get; set; }

    public int? BikeCompId { get; set; }

    public virtual BikeCompany? BikeComp { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Quotation> Quotations { get; } = new List<Quotation>();
}
