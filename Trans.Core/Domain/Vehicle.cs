using Microsoft.EntityFrameworkCore;

namespace Trans.Core.Domain
{
    public class Vehicle
    {
        public Guid VehicleId { get; protected set; }
        public string Brand { get; protected set; }
        public string Model { get; protected set; }
        public string MaxVehicleWeight { get; protected set;}
        public DateTime UpdateAt { get; protected set; }

        protected Vehicle()
        {
        }

        protected Vehicle(string brand, string model, string maxVehicleWeight, int numberVehicleOfCompany)
        {
            VehicleId = Guid.NewGuid();
            SetBrand(brand);
            SetModel(model);
            SetMaxVehicleWeight(maxVehicleWeight);
            UpdateAt = DateTime.UtcNow;
        }

        private void SetBrand(string brand)
        {
            if(string.IsNullOrWhiteSpace(brand))
            {
                throw new Exception("Put incorect data");
            }
            if(Model == brand)
            {
                return;
            }
            Brand = brand;
        }

        private void SetModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                throw new Exception("Put incorect data");
            }
            if (Model == model)
            {
                return;
            }
            Model = model;
        }

        private void SetMaxVehicleWeight(string maxVehicleWeight)
        {
            if (string.IsNullOrWhiteSpace(maxVehicleWeight))
            {
                throw new Exception("Put incorect data");
            }
            if (MaxVehicleWeight == maxVehicleWeight)
            {
                return;
            }
            MaxVehicleWeight = maxVehicleWeight;
        }

        public static Vehicle Create(string brand, string model, string maxVehicleWeight, int numberVehicleOfCompany)
            => new Vehicle(brand, model, maxVehicleWeight, numberVehicleOfCompany);
    }
}