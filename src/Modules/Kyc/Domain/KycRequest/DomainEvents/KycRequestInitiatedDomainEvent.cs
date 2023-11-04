using CXChange.BuildingBlocks.Domain;

namespace CXChange.Modules.Kyc.Domain.KycRequest.DomainEvents
{
    public class KycRequestInitiatedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; }
        public KycRequestInitiatedDomainEvent(KycRequest request)
        {
            UserId = request.GetSnapShot().Id;

        }
    }
}
