﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Commands
{
    public class CreateUser : ICommand
    {
        public string Email { get;  set; }
        public string Password { get;  set; }
        public string Username { get;  set; }
        public string Fullname { get;  set; }
        public string Role { get;  set; }
    }
}
