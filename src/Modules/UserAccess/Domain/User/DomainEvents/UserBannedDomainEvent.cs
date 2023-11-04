using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserBannedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; init; }

        public UserBannedDomainEvent(User user)
        {
            var snapshot = user.GetSnapShot();
            UserId = snapshot.Id;
        }
    }
}
