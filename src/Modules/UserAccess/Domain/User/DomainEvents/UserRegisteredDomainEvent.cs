using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserRegisteredDomainEvent:IDomainEvent
    {
        public UserRegisteredDomainEvent(User user)
        {
            var snapShot = user.GetSnapShot();
            PhoneNumber= snapShot.PhoneNumber;
            SecurityToken = snapShot.SecurityStamp;

            
        }
       public string PhoneNumber { get; private set; }
       public string SecurityToken { get;private set; }
     
    }
}
