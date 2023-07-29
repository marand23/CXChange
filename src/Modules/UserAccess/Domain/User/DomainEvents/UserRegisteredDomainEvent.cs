using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserRegisteredDomainEvent : IDomainEvent
    {
        public UserRegisteredDomainEvent(User user)
        {
            var snapShot = user.GetSnapShot();
            PhoneNumber = snapShot.PhoneNumber;
            SecurityToken = snapShot.SecurityStamp;


        }
        public string PhoneNumber { get; private set; }
        public string SecurityToken { get; private set; }

    }
}
