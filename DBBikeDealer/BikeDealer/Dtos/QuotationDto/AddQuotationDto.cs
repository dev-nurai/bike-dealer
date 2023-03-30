namespace BikeDealer.Dtos.QuotationDto
{
    public class AddQuotationDto
    {

        public int CustomerId { get; set; }
        public int? EmployeeId { get; set; }

        public string? QuoteDetails { get; set; }

        public DateTime? QuotationDate { get; set; }

        public int bikeModel { get; set; }

    }
}
