using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Infrastructure.DataAccess;
using System.Data;

namespace CXChange.Modules.Payment.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentDbContext _context;
        private readonly IDomainEventDispatcher _dispatcher;
        public UnitOfWork(PaymentDbContext context, IDomainEventDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }
        public async Task DispatchDomainEvents()
        {
            var domainEntities = _context.ChangeTracker
    .Entries<IEntity>()
    .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
    .SelectMany(x => x.Entity.DomainEvents).ToList();

            await _dispatcher.Dispatch(domainEntities);
            domainEntities.Clear();

        }
        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents();
            return await _context.SaveChangesAsync(cancellationToken);
        }


        public Task Clear()
        {
            _context.ChangeTracker.Clear();
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
