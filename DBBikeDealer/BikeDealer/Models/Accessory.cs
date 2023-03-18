using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class Accessory
{
    public int AccessoriesId { get; set; }

    public string? Name { get; set; }

    public long? Price { get; set; }
}
