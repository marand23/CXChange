using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public class UserTwoFactorTypeChangedDomainEvent : IDomainEvent
    {
        public UserTwoFactorTypeChangedDomainEvent(User user) { }
        public string UserId { get; private set; }

    }
}
