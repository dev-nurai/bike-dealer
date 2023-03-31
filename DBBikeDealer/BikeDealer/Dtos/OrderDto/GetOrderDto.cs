using BikeDealer.Dtos.QuotationDto;
using BikeDealer.Models;

namespace BikeDealer.Dtos.OrderDto
{
    public class GetOrderDto
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public BikeDetailsDto BikesDetailsDto { get; set; }
        public int BikeModelId { get; set; }
        public long? Price { get; set; }
        public string OrderStatus { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Remarks { get; set; }
        public string UpdatedBy { get; set; }
        public int UpdatedbyId { get; set;}

        public List<Accessory> Accessories { get; set; }

    }
    

}
