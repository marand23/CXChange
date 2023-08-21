using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserLoginCheckedDomainEvent : IDomainEvent
    {
        public UserLoginCheckedDomainEvent(User user)
        {
            SnapShot snapShot = user.GetSnapShot();
            PhoneNumber = snapShot.PhoneNumber;
            Id = snapShot.Id;
            SecurityStamp = snapShot.SecurityStamp;
            DefaultTwoFactor = snapShot.DefaultTwoFactor;
        }

        public string PhoneNumber { get; private set; }
        public string Id { get; private set; }
        public string SecurityStamp { get; private set; }
        public string DefaultTwoFactor { get; private set; }
    }
}
