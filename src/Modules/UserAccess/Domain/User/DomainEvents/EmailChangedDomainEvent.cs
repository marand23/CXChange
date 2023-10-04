using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.UserAccess.Domain.User.DomainEvents
{
    public record EmailChangedDomainEvent : IDomainEvent
    {
        public string Email { get; init; }
        public string UserId { get; init; }
        public EmailChangedDomainEvent(User user)
        {
            var snapshot = user.GetSnapShot();
            Email = snapshot.Email;
            UserId = snapshot.Id;
        }
    }
}
