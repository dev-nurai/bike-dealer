using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class BikeCompany
{
    public int BikeCompId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<BikeModel> BikeModels { get; } = new List<BikeModel>();
}
