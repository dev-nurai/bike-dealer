using BikeDealer.Dtos.CustomerDto;
using BikeDealer.Dtos.QuotationDto;

namespace BikeDealer.Dtos.OrderDto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? OrderDate { get; set; }
        public BikeDetailsDto BikesDetailsDto { get; set; }
        

        

    }

}
