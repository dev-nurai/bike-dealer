namespace BikeDealer.Dtos.QuotationDto
{
    public class QuotationDto
    {
        public int QuoteId { get; set; }
        public string? CustomerName { get; set; }
        public string? EmployeeName { get; set; }
        public string? QuoteDetails { get; set; }
        public DateTime? QuotationDate { get; set; }
        public string? Remarks { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public BikeDetailsDto BikeDetails { get; set; }


    }

    public class BikeDetailsDto
    {
        public string BikeName { get; set; }
        public string BikeModel { get; set;}
        public long? Price { get; set; }
    }





}
