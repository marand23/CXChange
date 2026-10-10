using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.Interfaces;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.Payment.Application.Commands.ShabaAdd
{
    internal class SubmitShabaHandler : IConsumer<SubmitShaba>
    {
        IUserRepository _repo;
        IUnitOfWork _uow;
        public SubmitShabaHandler(IUserRepository repo, [FromKeyedServices("payment")] IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task Consume(ConsumeContext<SubmitShaba> context)
        {
            var user = await _repo.FindById(context.Message.UserId);
            if (user == null) { }

            await _uow.CommitChangesAsync();
            await context.RespondAsync(new ShabaSubmitted { });

        }
    }
}
