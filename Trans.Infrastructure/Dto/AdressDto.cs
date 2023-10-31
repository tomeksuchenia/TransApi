using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Dto
{
    public class AdressDto
    {
        public string Country { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Street { get; protected set; }
        public string BuildingNumber { get; protected set; }
    }
}
