using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Kyc.Domain.User.DomainEvents
{
    public record KycRejectedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; init; }
        public KycRejectedDomainEvent(User user)
        {
            UserId = user.Id;
        }
    }
}
