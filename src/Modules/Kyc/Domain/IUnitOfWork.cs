namespace CXChange.Modules.Kyc.Domain
{
    public interface IUnitOfWork
    {
        Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}
