using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CXChange.Modules.UserManagment.Application
{
    internal class SendCommand:ISendCommand
    {
        private IMediator mediator;
        public SendCommand(IMediator mediator) {
            this.mediator = mediator;
        }
        public void Send(IRequest request) {
            mediator.Send(request);
        }
    }
}
