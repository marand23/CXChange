using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserPasswordResetCheckedDomainEvent : IDomainEvent
    {
        public UserPasswordResetCheckedDomainEvent(User user)
        {
            this.User = user;
        }
        public User User { get; }
    }
}
