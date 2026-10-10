using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Infrastructure.DataAccess;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        UserManagementDbContext _context;
        public UnitOfWork(UserManagementDbContext context)
        {
            _context = context;
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<int> CommitChangesAsyncWithConcurrencyCheck(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
