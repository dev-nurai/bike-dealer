namespace BikeDealer.Dtos.QuotationDto
{
    public class AddQuotationDto
    {
        public int QuoteId { get; set; }
        public int CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public string? QuoteDetails { get; set; }
        public DateTime? QuotationDate { get; set; }
        public int bikeModelId { get; set; }
        public string? Remarks { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int UpdateBy { get; set; }
    }
}
