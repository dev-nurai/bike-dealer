namespace BikeDealer.Dtos.QuotationDto
{
    public class GetQuotationbyIdDto
    {
        public int QuoteId { get; set; }

        public string? CustomerName { get; set; }
        public long? CustomerNumber { get; set; }

        public string? EmployeeName { get; set; }

        public string? QuoteDetails { get; set; }

        public DateTime? QuotationDate { get; set; }

        public BikeDetail BikeDetail { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string? Remarks { get; set; }

        public string? UpdatedBy { get; set; }


    }
    public class BikeDetail
    {
        public string BikeName { get; set; }
        public string BikeModel { get; set; }
        public long? Price { get; set; }
    }


}
