namespace BikeDealer.Dtos.OrderDto
{
    public class GetOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? OrderDate { get; set; }
        public OrderBikeDetails BikeDetails { get; set; }

        public string OrderStatus { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Remarks { get; set; }

        public string UpdatedBy { get; set; }

    }
    
    public class OrderBikeDetails
    {
        public string BikeName { get; set; }
        public string BikeModel { get; set; }
        public long? Price { get; set; }
    }

}
