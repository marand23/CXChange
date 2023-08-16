using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserRegistrationConfirmedDomainEvent : IDomainEvent
    {
        public UserRegistrationConfirmedDomainEvent(User user)
        {
            var snapshot = user.GetSnapShot();
            UserName = snapshot.UserName;
            PhoneNumber = snapshot.PhoneNumber;
        }
        public string UserName { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
