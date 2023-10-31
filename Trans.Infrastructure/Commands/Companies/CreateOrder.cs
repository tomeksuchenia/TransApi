using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Core.Domain;
using Trans.Infrastructure.Commands.Models;

namespace Trans.Infrastructure.Commands.Companies
{
    public class CreateOrder
    {
        public OrderCompanyOrder OrderCompany { get; set; }
        public LoadOrder Load { get; set; }
        public int PaymentDays { get; set; }
        public bool IsPaid { get; set; }
        public string StatusOrderDescription { get; set; }
        public DateTime StartDayTransport { get; set; }
        public DateTime EndDayTransport { get; set; }
    }
}
