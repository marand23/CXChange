using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record UserEmailDeactivatedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; init; }
        public UserEmailDeactivatedDomainEvent(User user)
        {
            var snapshot = user.GetSnapShot();
            UserId = snapshot.Id;
        }
    }
}
