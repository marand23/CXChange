using MassTransit.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    public record UserRegistered
    {
        public string PhoneNumber { get; set;}
        public string Email { get; set;}
    }
}
