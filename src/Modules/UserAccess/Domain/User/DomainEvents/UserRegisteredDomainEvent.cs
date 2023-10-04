using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserRegisteredDomainEvent : IDomainEvent
    {
        public UserRegisteredDomainEvent(User user)
        {
            this.User = user;


        }
        public User User { get; init; }

    }
}
