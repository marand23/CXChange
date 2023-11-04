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
            UserId = snapshot.Id;
        }
        public string UserName { get; init; }
        public string PhoneNumber { get; init; }
        public Guid UserId { get; init; }
    }
}
