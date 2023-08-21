using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserPasswordResetCheckedDomainEvent : IDomainEvent
    {
        public UserPasswordResetCheckedDomainEvent(User user)
        {
            var snapshot = user.GetSnapShot();
            PhoneNumber = snapshot.PhoneNumber;
            Id = snapshot.Id;
            SecurityStamp = snapshot.SecurityStamp;
        }
        public string PhoneNumber { get; }
        public string Id { get; }
        public string SecurityStamp { get; }
    }
}
