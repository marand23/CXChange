using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain;
using CXChange.Modules.Kyc.Infrastructure.DataAccess;

namespace CXChange.Modules.Kyc.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        KycDbContext _dbContext;
        IDomainEventDispatcher _dispatcher;
        public UnitOfWork(KycDbContext context, IDomainEventDispatcher dispatcher)
        {
            _dbContext = context;
            _dispatcher = dispatcher;
        }
        public void DispatchDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
    .Entries<IEntity>()
    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
    .SelectMany(x => x.Entity.DomainEvents).ToList();

            _dispatcher.Dispatch(domainEntities);
            domainEntities.Clear();

        }
        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            DispatchDomainEvents();
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
