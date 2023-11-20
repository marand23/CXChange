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
        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
