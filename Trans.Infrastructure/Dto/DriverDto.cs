namespace Trans.Infrastructure.Dto
{
    public class DriverDto
    {
        public Guid DriverId { get;  set; }
        public string Name { get;  set; }
        public string Fullname { get;  set; }
        public string TelephoneNumer { get;  set; }
        public VehicleDto Vehicle { get;  set; }
    }
}