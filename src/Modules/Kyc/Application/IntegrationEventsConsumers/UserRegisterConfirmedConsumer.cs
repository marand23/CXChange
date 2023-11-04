using CXChange.Modules.Kyc.Domain;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Domain.User;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;

namespace CXChange.Modules.Kyc.Application.IntegrationEventsConsumers
{
    internal class UserRegisterConfirmedConsumer : IConsumer<UserRegistrationConfiremedIntegrationEvent>
    {
        IUserRepository _repo;
        IUnitOfWork _uow;
        public UserRegisterConfirmedConsumer(IUserRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task Consume(ConsumeContext<UserRegistrationConfiremedIntegrationEvent> context)
        {
            var user = User.CreateUser(context.Message.UserId);
            await _repo.Add(user);
            await _uow.CommitChangesAsync();


        }
    }
}
