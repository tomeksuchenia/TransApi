namespace Trans.Core.Domain
{
    public class Driver 
    {
        public Guid DriverId { get; protected set; }
        public string Name { get; protected set; }
        public string Fullname { get; protected set; }
        public string TelephoneNumer { get; protected set; }
        

        protected Driver()
        {
        }

        protected Driver(string name, string fullname, string telephoneNumber)
        {
            DriverId = Guid.NewGuid();
            Name = name;
            Fullname = fullname;
            TelephoneNumer = telephoneNumber;
        }

        public static Driver Create(string name, string fullname, string telephoneNumber)
            => new Driver(name, fullname, telephoneNumber);
    }
}