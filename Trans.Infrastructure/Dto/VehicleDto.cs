namespace Trans.Infrastructure.Dto
{
    public class VehicleDto
    {
        public Guid VehicleId { get; set; }
        public string Brand { get;  set; }
        public string Model { get;  set; }
        public string MaxVehicleWeight { get;  set; }
        public DateTime UpdateAt { get;  set; }
    }
}