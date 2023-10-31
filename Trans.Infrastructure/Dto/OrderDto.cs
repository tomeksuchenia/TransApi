using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Dto
{
    public class OrderDto
    {
        public string OrderId { get;  set; }
        public OrderCompanyDto OrderCompany { get;  set; }
        public LoadDto Load { get;  set; }
        public int PaymentDays { get;  set; }
        public bool IsPaid { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public DateTime StartDayTransport { get;  set; }
        public DateTime EndDayTransport { get;  set; }
        public DateTime EndDayPayment { get;  set; }
    }
}
