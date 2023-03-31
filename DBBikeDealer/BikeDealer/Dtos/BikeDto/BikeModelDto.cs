namespace BikeDealer.Dtos.BikeDto
{
    public class BikeModelDto
    {
        public int BikeModelId { get; set; }

        public int BikeCompId { get; set; }

        public string BikeModel { get; set; }

        public short? ModelYear { get; set;}
        public long Price { get; set; }

        public BikeCompanyDto BikeCompanyDetails { get; set; }
    }
}
