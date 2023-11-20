using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestReject
{
    internal class RejectKycRequestHandler : IConsumer<RejectKycRequest>
    {
        IUserRepository _userRepo;

        IDomainEventDispatcher _dispatcher;
        public RejectKycRequestHandler(IUserRepository userRepo, IDomainEventDispatcher dispatcher)
        {
            _userRepo = userRepo;
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<RejectKycRequest> context)
        {
            var user = await _userRepo.FindUserById(context.Message.UserId);
            user.RejectKyc();

            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new KycRequestRejected { });

        }
    }
}
