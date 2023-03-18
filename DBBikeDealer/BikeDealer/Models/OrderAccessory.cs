using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class OrderAccessory
{
    public int? OrderId { get; set; }

    public int? AccessoriesId { get; set; }

    public virtual Accessory? Accessories { get; set; }

    public virtual Order? Order { get; set; }
}
