using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Core.Domain
{
    public static class ErrorCodes
    {
        public static string DriverNotFound => "driver_not_found";
        public static string VehicleNotFound => "vehicle_not_found";
        public static string InvalidRole => "invalid_role";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidTelephoneNumber => "invalid_telephone_number";
        public static string InvalidPaymentDays => "invalid_amount_payment_day";
        public static string InvalidOrderId => "invalid_order_id";

    }
}
