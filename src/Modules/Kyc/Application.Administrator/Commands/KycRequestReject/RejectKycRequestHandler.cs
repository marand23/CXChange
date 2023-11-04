using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestReject
{
    internal class RejectKycRequestHandler : IConsumer<RejectKycRequest>
    {
        IUserRepository _userRepo;
        IKycRequestRepository _kycRequestRepo;
        IDomainEventDispatcher _dispatcher;
        public RejectKycRequestHandler(IUserRepository userRepo, IKycRequestRepository kycRequestRepo, IDomainEventDispatcher dispatcher)
        {
            _userRepo = userRepo;
            _kycRequestRepo = kycRequestRepo;
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<RejectKycRequest> context)
        {
            var user = await _userRepo.FindUserById(context.Message.UserId);
            user.RejectKyc();
            var kycRequest = await _kycRequestRepo.FindByUserId(context.Message.UserId);
            kycRequest.MarkKycRequestAsStale();
            await _userRepo.UpdateUser(user);
            await _kycRequestRepo.Update(kycRequest);
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new KycRequestRejected { });

        }
    }
}
