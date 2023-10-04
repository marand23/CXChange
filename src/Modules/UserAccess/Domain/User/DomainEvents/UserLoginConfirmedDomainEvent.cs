using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserLoginConfirmedDomainEvent : IDomainEvent
    {
        public UserLoginConfirmedDomainEvent(User user)
        {
            var snapShot = user.GetSnapShot();
            Id = snapShot.Id;
            PhoneNumber = snapShot.PhoneNumber;
        }
        public string Id { get; init; }
        public string PhoneNumber { get; init; }

    }
}
