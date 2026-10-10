using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserLoginCheckedDomainEvent : IDomainEvent
    {
        public UserLoginCheckedDomainEvent(User user)
        {
            this.User = user;

        }

        public User User { get; init; }


    }
}
