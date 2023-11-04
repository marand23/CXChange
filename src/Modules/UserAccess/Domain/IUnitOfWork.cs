namespace CXChange.Modules.UserAccess.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);

    }
}
