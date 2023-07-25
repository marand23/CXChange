using CXChange.BuildingBlocks.Application.Command;
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
        public TwoFactorType TwoFactorType { get; set;}
    }
    public enum TwoFactorType
    {
        SMS,
        Email,
        Authenticator
    }
}
