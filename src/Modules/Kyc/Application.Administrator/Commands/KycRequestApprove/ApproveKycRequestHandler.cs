using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestApprove
{

    internal class ApproveKycRequestHandler : IConsumer<ApproveKycRequest>
    {
        IUserRepository _userRepo;
        IDomainEventDispatcher _dispatcher;

        public ApproveKycRequestHandler(IUserRepository userRepo, IDomainEventDispatcher dispatcher)
        {
            _userRepo = userRepo;
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<ApproveKycRequest> context)
        {
            var user = await _userRepo.FindUserById(context.Message.UserId);
            user.ApproveKyc();
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new KycRequestApproved { });
        }
    }
}
