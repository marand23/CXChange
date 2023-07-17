using CXChange.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserRegisteredDomainEvent:IDomainEvent
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }   
     
    }
}
