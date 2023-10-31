using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Commands.Models
{
    public class OrderCompanyOrder
    {
        public string Name { get;  set; }
        public AdressOrder Adress { get;  set; }
        public string? TelephoneNumber { get;  set; }
    }
}
