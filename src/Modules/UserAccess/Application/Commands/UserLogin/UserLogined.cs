using MassTransit.SagaStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.Commands.UserLogin
{
   public record UserLogined
    {
       public string Token { get; set; }
    }
}
