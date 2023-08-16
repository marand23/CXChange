using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public class UserLoginCheckedDomainEvent : IDomainEvent
    {
        public UserLoginCheckedDomainEvent(User user)
        {
            SnapShot snapShot = user.GetSnapShot();
            PhoneNumber = snapShot.PhoneNumber;
            Id = snapShot.Id;
        }

        public string PhoneNumber { get; private set; }
        public string Id { get; private set; }
    }
}
