namespace BikeDealer.Dtos.QuotationDto
{
    public class GetQuotationDto
    {
        public int QuoteId { get; set; }

        public string? CustomerName { get; set; }

        public string? EmployeeName { get; set; }

        public string? QuoteDetails { get; set; }

        public DateTime? QuotationDate { get; set; }

        public BikeDetails BikeDetails { get; set; }

    }

    public class BikeDetails
    {
        public string BikeName { get; set; }
        public string BikeModel { get; set;}
        public long? Price { get; set; }
    }





}
