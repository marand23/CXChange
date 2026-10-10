using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Interfaces;
using CXChange.Modules.Payment.Domain.User;
using CXChange.Modules.UserAccess.Integration.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Application.EventConsumers.Integration
{
    internal class UserRegisterConfirmedConsumer : IConsumer<UserRegistrationConfiremedIntegrationEvent>
    {
        public IUserRepository _repo;
        public IUnitOfWork _uow;
        public UserRegisterConfirmedConsumer(IUserRepository repo, [FromKeyedServices("payment")] IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task Consume(ConsumeContext<UserRegistrationConfiremedIntegrationEvent> context)
        {

            var user = User.CreateUser(context.Message.PhoneNumber, context.Message.UserId);
            await _repo.Add(user);
            await _uow.CommitChangesAsync();
        }
    }
}
