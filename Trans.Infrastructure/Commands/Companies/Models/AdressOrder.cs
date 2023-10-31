using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Commands.Models
{
    public class AdressOrder
    {
        public string Country { get;  set; }
        public string City { get;  set; }
        public string PostalCode { get;  set; }
        public string Street { get;  set; }
        public string BuildingNumber { get;  set; }
    }
}
