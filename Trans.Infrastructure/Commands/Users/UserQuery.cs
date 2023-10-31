using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Commands.Users
{
    public class UserQuery : ICommand
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
    }
}
