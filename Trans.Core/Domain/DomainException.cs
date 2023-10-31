using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Exceptions
{
    public class DomainException : Exception
    {
        public string Code { get; set; }
        public DomainException() : base()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string code, string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            Code = code;
        }
    }
}
