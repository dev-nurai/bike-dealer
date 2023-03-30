namespace BikeDealer.Dtos.BikeDto
{
    public class EditBikeModelDto
    {
        public int BikeModelId { get; set; }
        public int BikeCompId { get; set; }
        public string BikeModelName { get; set; }

        public short ModelYear { get; set; }

        public int Price { get; set; }
    }
}
