namespace BikeDealer.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public long? Price { get; set; }

        public DateTime? OrderDate { get; set; }

        public string SoldBy { get; set; }
        public string OrderBy { get; set; }

        public string BikeModel { get; set; }
        public Bike BikeDetails { get; set; }

        //public int Accessories { get; set; }

    }

    public class Bike
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
    }
}
