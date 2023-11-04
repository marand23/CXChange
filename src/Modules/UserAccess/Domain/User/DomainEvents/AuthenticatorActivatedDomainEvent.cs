using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public class AuthenticatorActivatedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; init; }
        public AuthenticatorActivatedDomainEvent(User user)
        {
            var snapshot = user.GetSnapShot();
            UserId = snapshot.Id;
        }
    }
}
