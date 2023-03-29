using BikeDealer.Models;

namespace BikeDealer.Dtos.CustomerDto
{
    public class CustomerDto
    {
        public string? Name { get; set; }

        public long? Number { get; set; }

        public string? Email { get; set; }

        public DateTime? DateOfQuotation { get; set; }

        public OrderDetails OrderDetails { get; set; }

    }

    public class OrderDetails
    {

        public string EmployeeName { get; set; }

        public string BikeName { get; set; }


    }
}



