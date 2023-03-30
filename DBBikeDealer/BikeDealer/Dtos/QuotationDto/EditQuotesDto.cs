namespace BikeDealer.Dtos.QuotationDto
{
    public class EditQuotesDto
    {
        public int QuoteId { get; set; }
        public string? QuoteDetails { get; set; }

        public int? bikeModelId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? Remarks { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
