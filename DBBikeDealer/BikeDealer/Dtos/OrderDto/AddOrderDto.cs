namespace BikeDealer.Dtos.OrderDto
{
    public class AddOrderDto
    {
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int BikeModelId { get; set; }
        public long? Price { get; set; }
        public DateTime? OrderDate { get; set; }

        //public int OrderStatus { get; set; } = 1;




    }
}
