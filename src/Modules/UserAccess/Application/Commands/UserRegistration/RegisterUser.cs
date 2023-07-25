
using CXChange.BuildingBlocks.Application;
using CXChange.BuildingBlocks.Application.Command;
using MassTransit.Mediator;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    public record  RegisterUser 
    {
        public string PhoneNumber { get; init; }
        public string Password { get; init; }
    }
}
