using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Application
{
    internal interface ISendCommand
    {
        void Send(IRequest request);
    }
}
