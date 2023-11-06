using CXChange.Modules.Kyc.Domain.Interfaces;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycPhotoDownlaod
{
    internal class DownloadKycPhotoHandler : IConsumer<DownloadKycPhoto>
    {
        IKycRequestRepository _repo;
        IEnumerable<IkycPhotoActions> _actions;
        public DownloadKycPhotoHandler(IKycRequestRepository repo, IEnumerable<IkycPhotoActions> actions
        )
        {
            _repo = repo;
            _actions = actions;
        }
        public async Task Consume(ConsumeContext<DownloadKycPhoto> context)
        {
            var kycRequest = await _repo.FindByUserId(context.Message.UserId);
            if (kycRequest == null) { }
            var defaultStorage = StorageTypeEnum.GoogleDrive;
            var downloader = _actions.FirstOrDefault(x => x.StorageType == defaultStorage);
            byte[] photo = await downloader.DownloadKycPhoto(kycRequest.GetSnapShot().KycPhotoId);
            await context.RespondAsync(new KycPhotoDownloaded { Photo = photo, NationalId = kycRequest.GetSnapShot().NationalId });
        }
    }
}
