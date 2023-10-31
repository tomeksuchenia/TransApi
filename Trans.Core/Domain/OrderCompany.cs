using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trans.Infrastructure.Exceptions;

namespace Trans.Core.Domain
{
    public class OrderCompany
    {
        private static readonly Regex TelephoneNumberRegex = new Regex("[0-9]");
        public string Name { get; protected set; }
        public Adress Adress { get; protected set; }
        public string? TelephoneNumber { get; protected set; }

        protected OrderCompany()
        {
        }

        protected OrderCompany(string name, Adress adress, string telephoneNumber)
        {
            Name = name;
            Adress = adress;
            SetTelephoneNumber(telephoneNumber);
        }

        private void SetTelephoneNumber(string telephoneNumber)
        {
            if (!TelephoneNumberRegex.IsMatch(telephoneNumber))
            {
                throw new DomainException(ErrorCodes.InvalidTelephoneNumber, "Telehone number is invalid");
            }
            TelephoneNumber = telephoneNumber;
        }

        public static OrderCompany Create(string name, Adress adress, string telephoneNumber)
            => new OrderCompany(name, adress, telephoneNumber);
    }
}
