namespace CXChange.Modules.Kyc.Domain.Interfaces.Repositories
{
    public interface IKycRequestRepository
    {
        Task<KycRequest.KycRequest> FindByUserId(Guid UserId);

        Task AddAsync(KycRequest.KycRequest request);
        Task Remove(Guid UserId);
        Task Update(KycRequest.KycRequest request);

    }
}
