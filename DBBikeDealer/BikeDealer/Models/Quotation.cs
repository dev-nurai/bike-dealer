using System;
using System.Collections.Generic;

namespace BikeDealer.Models;

public partial class Quotation
{
    public int QuoteId { get; set; }

    public int? CustId { get; set; }

    public int? EmpId { get; set; }

    public int? BikeModelId { get; set; }

    public string? QuoteDetails { get; set; }

    public DateTime? QuotationDate { get; set; }

    public virtual BikeModel? BikeModel { get; set; }

    public virtual Customer? Cust { get; set; }

    public virtual Employee? Emp { get; set; }
}
