using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trans.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandlerAsync(T command);
    }
}
