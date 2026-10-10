using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Kyc.Domain.User.DomainEvents
{
    public record LandLineConfirmedDomainEvent : IDomainEvent
    {
        Guid UserId { get; init; }
        public LandLineConfirmedDomainEvent(User user)
        {
            UserId = user.Id;
        }
    }
}
