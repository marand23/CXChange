using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.Kyc.Application.IntegrationEventsConsumers
{
    internal class LandLineConfirmedConsumer : IConsumer<LandLineCodeVerifiedIntegrationEvent>
    {
        private readonly IUserRepository _repo;
        private IUnitOfWork _uow;
        private readonly IDomainEventDispatcher _dispatcher;
        public LandLineConfirmedConsumer(IUserRepository repo, IDomainEventDispatcher dispatcher, IUnitOfWork uow)
        {
            _repo = repo;
            _dispatcher = dispatcher;
            _uow = uow;
        }
        public async Task Consume(ConsumeContext<LandLineCodeVerifiedIntegrationEvent> context)
        {
            var user = await _repo.FindUserById(context.Message.UserId);
            if (user == null)
            {

            }
            user.VerifyLandLine(context.Message.LandLine);
            await _repo.UpdateUser(user);
            await _dispatcher.Dispatch(user.DomainEvents);
            await _uow.CommitChangesAsync();

        }
    }
}
