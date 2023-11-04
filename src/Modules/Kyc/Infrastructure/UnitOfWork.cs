using CXChange.Modules.Kyc.Domain;
using CXChange.Modules.Kyc.Infrastructure.DataAccess;

namespace CXChange.Modules.Kyc.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        KycDbContext _dbContext;
        public UnitOfWork(KycDbContext context)
        {
            _dbContext = context;
        }
        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
