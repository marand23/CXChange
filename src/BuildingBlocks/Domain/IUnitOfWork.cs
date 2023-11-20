namespace CXChange.BuildingBlocks.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}
