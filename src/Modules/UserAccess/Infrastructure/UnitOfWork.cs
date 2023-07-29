using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using MassTransit.Mediator;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly UserManagementDbContext _userManagementDbContext;
        private readonly IMediator _mediator;
        public UnitOfWork(UserManagementDbContext userManagementDbContext, IMediator mediator)
        {
            _userManagementDbContext = userManagementDbContext;
            _mediator = mediator;

        }
        public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
        {

            return await _userManagementDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _userManagementDbContext.Dispose();
        }


    }
}
