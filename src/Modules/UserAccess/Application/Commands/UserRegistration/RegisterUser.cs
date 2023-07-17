
using MassTransit.Mediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    public record  RegisterUser:IRequest<UserRegistered>
    {
        public string PhoneNumber { get; init; }
        public string Password { get; init; }
    }
}
