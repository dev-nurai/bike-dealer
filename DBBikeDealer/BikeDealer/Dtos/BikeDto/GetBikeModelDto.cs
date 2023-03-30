namespace BikeDealer.Dtos.BikeDto
{
    public class GetBikeModelDto
    {
        public int Id { get; set; }

        public string BikeName { get; set; }

        public string BikeModel { get; set; }

        public short? ModelYear { get; set;}
        public long Price { get; set; }


    }
}
