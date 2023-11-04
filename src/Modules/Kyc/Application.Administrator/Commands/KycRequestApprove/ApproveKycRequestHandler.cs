using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycRequestApprove
{

    internal class ApproveKycRequestHandler : IConsumer<ApproveKycRequest>
    {
        IUserRepository _userRepo;
        IKycRequestRepository _kycRequestRepo;
        IDomainEventDispatcher _dispatcher;

        public ApproveKycRequestHandler(IUserRepository userRepo, IKycRequestRepository kycRequestRepo, IDomainEventDispatcher dispatcher)
        {
            _userRepo = userRepo;
            _kycRequestRepo = kycRequestRepo;
            _dispatcher = dispatcher;
        }

        public async Task Consume(ConsumeContext<ApproveKycRequest> context)
        {
            var kycRequest = await _kycRequestRepo.FindByUserId(context.Message.UserId);
            var user = await _userRepo.FindUserById(context.Message.UserId);
            var isNationalIdUnique = await _userRepo.IsNationalIdUnique(kycRequest.GetSnapShot().NationalId);
            user.PreCheckKycRequestApproval(isNationalIdUnique);
            var kycReqSnapshot = kycRequest.GetSnapShot();
            user.ApproveKyc(kycReqSnapshot.NationalId, kycReqSnapshot.Name, kycReqSnapshot.SurName, kycReqSnapshot.BirthDate);
            kycRequest.MarkKycRequestAsStale();
            await _userRepo.UpdateUser(user);
            await _kycRequestRepo.Update(kycRequest);
            await _dispatcher.Dispatch(user.DomainEvents);
            await context.RespondAsync(new KycRequestApproved { });


        }
    }
}
