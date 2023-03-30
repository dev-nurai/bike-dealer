namespace BikeDealer.Dtos.OrderDto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? EmployeeName { get; set; }
        
        public DateTime? OrderDate { get; set; }
        public Bike BikeDetails { get; set; }

        //public int Accessories { get; set; }

    }

    public class Bike
    {
        public string BikeName { get; set; }
        public string BikeModel { get; set; }
        public long? Price { get; set; }
    }
}
