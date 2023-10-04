using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record AuthenticatorDeactivatedDomainEvent : IDomainEvent
    {
        public string UserId { get; init; }
        public AuthenticatorDeactivatedDomainEvent(User user)
        {
            SnapShot snapShot = user.GetSnapShot();
            UserId = snapShot.Id;
        }
    }
}
