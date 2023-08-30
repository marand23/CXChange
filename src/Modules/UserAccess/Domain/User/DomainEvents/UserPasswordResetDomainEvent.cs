using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public class UserPasswordResetDomainEvent : IDomainEvent
    {
        public UserPasswordResetDomainEvent(User user)
        {
            var snapshot = user.GetSnapShot();
            PhoneNumber = snapshot.PhoneNumber;
            Id = snapshot.Id;
        }
        public string PhoneNumber { get; set; }
        public string Id { get; set; }
    }
}
