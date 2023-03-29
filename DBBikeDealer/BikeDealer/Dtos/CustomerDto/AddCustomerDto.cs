namespace BikeDealer.Dtos.Customer
{
    public class AddCustomerDto
    {
        public string? Name { get; set; }

        public long? Number { get; set; }

        public string? Email { get; set; }
        public DateTime? DateOfQuotation { get; set; }
    }
}
