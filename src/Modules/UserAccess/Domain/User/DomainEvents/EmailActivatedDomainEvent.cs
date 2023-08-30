using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public class EmailActivatedDomainEvent : IDomainEvent
    {
        public string Email { get; }
        public string Id { get; }

        public EmailActivatedDomainEvent(User user)
        {
            SnapShot snapShot = user.GetSnapShot();
            Email = snapShot.Email;
            Id = snapShot.Id;
        }
    }
}
