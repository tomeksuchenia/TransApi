﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Commands.Users
{
    public class ChangeUserPassword
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string repeatNewPassword { get; set; }
    }
}
