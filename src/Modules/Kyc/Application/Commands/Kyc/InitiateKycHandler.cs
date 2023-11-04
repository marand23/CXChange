using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Application.ExternalApiServices;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Domain.KycRequest;
using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using MassTransit;

namespace CXChange.Modules.Kyc.Application.Commands.Kyc
{

    internal class InitiateKycHandler : IConsumer<InitiateKyc>
    {
        IUserRepository _repo;
        IEnumerable<IKycPhotoActions> _kycPhotoActions;
        IDomainEventDispatcher _dispatcher;
        IKycRequestRepository _kycRequestRepository;

        public InitiateKycHandler(IEnumerable<IKycPhotoActions> kycPhotoActions, IUserRepository repo, IDomainEventDispatcher dispatcher, IKycRequestRepository kycRequest)
        {
            _kycPhotoActions = kycPhotoActions;
            _repo = repo;
            _dispatcher = dispatcher;
            _kycRequestRepository = kycRequest;
            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<InitiateKyc> context)
        {

            var defaultStorate = StorageTypeEnum.GoogleDrive;

            var user = await _repo.FindUserById(context.Message.UserId);
            if (user == null)
            {

            }
            user.PreCheckKycInitialization(await _repo.IsNationalIdUnique(context.Message.NationalId));
            var message = context.Message;
            var kycrequest = KycRequest.CreateKycRequest(message.UserId, message.Name, message.SurName, message.NationalId, message.BithDate);
            var uploader = _kycPhotoActions.FirstOrDefault(x => x.StorageType == defaultStorate);
            var result = await uploader.UploadKycPhoto(context.Message.Image);
            kycrequest.SetKycRequestPhotoId(result);
            kycrequest.SetKycRequestStorageType(defaultStorate);
            user.ChangeKycStateToInProgress();
            await _kycRequestRepository.AddAsync(kycrequest);
            await _repo.UpdateUser(user);
            await context.RespondAsync(new KycInitiated { });
        }

    }
}
