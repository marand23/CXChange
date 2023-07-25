using CXChange.BuildingBlocks.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Application.Commands.UserLogin
{
    public record LoginUser:CommandBase
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
