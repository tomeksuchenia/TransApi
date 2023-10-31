using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Exceptions
{
    public static class ErrorCodesService
    {
        public static string EmailInUse => "email_in_use";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidCredentials => "invalid_credentials";
        public static string CompanyNotFound => "company_not_found";
        public static string CompanyAlreadyExist => "company_already_exist";
        public static string UserNotFound => "user_not_found ";

    }
}

