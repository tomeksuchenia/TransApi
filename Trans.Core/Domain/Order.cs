using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrastructure.Exceptions;

namespace Trans.Core.Domain
{
    public class Order
    {
        public int OrderId { get; protected set; }
        public OrderCompany OrderCompany { get; protected set; }
        public Load Load { get; protected set; }
        public int PaymentDays { get; protected set; }
        public bool IsPaid { get; protected set; }
        public string StatusOrderDescription { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime StartDayTransport { get; protected set; }
        public DateTime EndDayTransport { get; protected set; }
        public DateTime EndDayPayment { get; protected set; }

        protected Order()
        {
        }

        public Order(OrderCompany orderCompany, Load load, int paymentDays, bool isPaid, string statusOrderDescription,
            DateTime startDayTransport, DateTime endDayTransport)
        {
            OrderCompany = orderCompany;
            Load = load;
            PaymentDays = paymentDays;
            SetPaid(isPaid);
            SetStatusOrderDescription(statusOrderDescription);
            CreatedAt = DateTime.Now;
            StartDayTransport = startDayTransport;
            EndDayTransport = endDayTransport;
            EndDayPayment = EndDayTransport.AddDays(paymentDays);
        }

        private void SetStatusOrderDescription(string statusOrderDescription)
        {
            if (String.IsNullOrEmpty(statusOrderDescription))
            {
                throw new DomainException("Status description can't be null");
            }

            StatusOrderDescription = statusOrderDescription;
        }

        private void SetPaymentsDay(int paymentsDays)    
        {
            if(paymentsDays < 0)
            {
                throw new DomainException(ErrorCodes.InvalidPaymentDays, "Invalid amount payment day");
            }
            PaymentDays = paymentsDays;
        }

        public void SetPaid(bool isPaid)
        {
            IsPaid = isPaid;
        }

    }
}
