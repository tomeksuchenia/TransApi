using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Exceptions;

namespace Trans.Core.Domain
{
    public class Company : Entity
    {
        private ISet<Driver> _drivers = new HashSet<Driver>();
        private ISet<Vehicle> _vehicle = new HashSet<Vehicle>();
        private ISet<Order> _orders = new HashSet<Order>();
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public Adress Adress { get; protected set; }
        public string TaxNumber { get; protected set; }
        public int CountVehicle { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<Driver> Drivers
        {
            get { return _drivers; }
            set { _drivers = new HashSet<Driver>(value); }
        }
        public IEnumerable<Vehicle> Vehicles
        {
            get { return _vehicle; }
            set { _vehicle = new HashSet<Vehicle>(value); }
        }
        public IEnumerable<Order> Orders
        {
            get { return _orders; }
            set { _orders = new HashSet<Order>(value); }
        }

        protected Company()
        {
        }

        public Company(Guid id, string name, string email, Adress adress, string taxNumber)
        {
            Id = id;
            Email = email;
            Adress = adress;
            SetName(name);
            SetTaxNumber(taxNumber);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Put incorect data.");
            }
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetTaxNumber(string taxNumber)
        {
            if(string.IsNullOrWhiteSpace(taxNumber))
            {
                throw new Exception("Put incorect data.");
            }
            TaxNumber = taxNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAdress(string country, string city, string postalCode, string street, string buildingNumber)
        {
            Adress.Create(country, city, postalCode, street, buildingNumber);
        }

        public void AddDriver(string name, string fullname, string telephoneNumber)
        {
            _drivers.Add(Driver.Create(name, fullname, telephoneNumber));
            UpdatedAt = DateTime.UtcNow;
        }
        public void DeleteDriver(Guid id)
        {
            var driver = Drivers.SingleOrDefault(x => x.DriverId == id);
            if (driver == null)
            {
                throw new DomainException(ErrorCodes.DriverNotFound, $"Driver with id: {driver.DriverId} not exist.");
            }
            _drivers.Remove(driver);
            UpdatedAt = DateTime.UtcNow;
        }
        public void AddVehicle(string brand, string model, string maxVehicleWeight)
        {
            _vehicle.Add(Vehicle.Create(brand, model, maxVehicleWeight, CountVehicle++));
            CountVehicle = CountVehicle++;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteVehicle(Guid vehicleId)
        {
            var vehicle = Vehicles.SingleOrDefault(x => x.VehicleId == vehicleId);
            if(vehicle == null)
            {
                throw new Exception($"Vehicle with number: {vehicleId} was not found.");
            }
            _vehicle.Remove(vehicle);
            UpdatedAt = DateTime.UtcNow;
        }

        public Order GetOrder(int id)
        {
            var order = Orders.SingleOrDefault(x => x.OrderId == id);
            if (order == null)
            {
                throw new DomainException("order_not_exist", $"Order with id: {id} not exist");
            }

            return order;
        }

        public void AddOrder(Order order)
        {
            var orderGet = _orders.SingleOrDefault(x => x.OrderId == order.OrderId);
            if (orderGet != null)
            {
                throw new DomainException(ErrorCodes.InvalidOrderId, $"Order with id: {order.OrderId} already exist");
            }
            _orders.Add(order);
            UpdatedAt= DateTime.UtcNow; 
        }

        public void DeleteOrder(int orderId)
        {
            var orderGet = _orders.SingleOrDefault(x => x.OrderId == orderId);
            if (orderGet == null)
            {
                throw new DomainException(ErrorCodes.InvalidOrderId, $"Order with id: {orderId} not exist");
            }
            _orders.Remove(orderGet);
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPaidStatus(int orderId, bool isPaid)
        {
            var orderGet = _orders.SingleOrDefault(x => x.OrderId == orderId);
            if (orderGet == null)
            {
                throw new DomainException(ErrorCodes.InvalidOrderId, $"Order with id: {orderId} not exist");
            }
            orderGet.SetPaid(isPaid);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
