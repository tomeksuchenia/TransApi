using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Exceptions
{
    public class ServiceException : Exception
    {
        public string Code { get; set; }
        public ServiceException() : base()
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string code, string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            Code = code;
        }
    }
}
