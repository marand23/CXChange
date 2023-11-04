using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Kyc.Domain.User.DomainEvents
{
    public record KycApprovedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; init; }
        public KycApprovedDomainEvent(User user)
        {
            UserId = user.GetSnapshot().Id;
        }
    }
}
