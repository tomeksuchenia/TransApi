using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Core.Domain
{
    [Owned]
    public class Adress
    {  
        public string Country { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Street { get; protected set; }
        public string BuildingNumber { get; protected set; }

        protected Adress()
        { 
        }

        protected Adress(string country, string city, string postalCode, string street, string buildingNumber)
        {
            SetCountry(country);
            SetCity(city);
            SetPostalCode(postalCode);
            SetStreet(street);
            SetBuildingNumber(buildingNumber);
        }
        private void SetCountry(string country)
        {
            if(string.IsNullOrWhiteSpace(country))
            {
                throw new Exception($"Put incorect data.");
            }
            if(Country == country)
            {
                return;
            }
            Country = country;
        }

        private void SetCity(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new Exception($"Put incorect data.");
            }
            if (City == city)
            {
                return;
            }
            City = city;
        }

        private void SetPostalCode(string postalCode)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new Exception($"Put incorect data.");
            }
            if (PostalCode == postalCode)
            {
                return;
            }
            PostalCode = postalCode;
        }
        private void SetStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new Exception($"Put incorect data.");
            }
            if (Street == street)
            {
                return;
            }
            Street = street;
        }
        private void SetBuildingNumber(string buildingNumber)
        {
            if (string.IsNullOrWhiteSpace(buildingNumber))
            {
                throw new Exception($"Put incorect data.");
            }
            if (BuildingNumber == buildingNumber)
            {
                return;
            }
            BuildingNumber = buildingNumber;
        }

        public static Adress Create(string country, string city, string postalCode, string street, string buildingNumber)
            => new Adress(country, city, postalCode, street, buildingNumber);
    }
}
