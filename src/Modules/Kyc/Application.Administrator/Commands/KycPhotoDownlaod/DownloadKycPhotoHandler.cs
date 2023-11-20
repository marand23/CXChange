using CXChange.Modules.Kyc.Domain.Interfaces;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Domain.User.Enums;
using MassTransit;

namespace CXChange.Modules.Kyc.Administrator.Application.Commands.KycPhotoDownlaod
{
    internal class DownloadKycPhotoHandler : IConsumer<DownloadKycPhoto>
    {

        IEnumerable<IkycPhotoActions> _actions;
        IUserRepository _repo;
        public DownloadKycPhotoHandler(IEnumerable<IkycPhotoActions> actions
        )
        {

            _actions = actions;
        }
        public async Task Consume(ConsumeContext<DownloadKycPhoto> context)
        {
            var user = await _repo.FindUserById(context.Message.UserId);
            var defaultStorage = StorageTypeEnum.GoogleDrive;
            var downloader = _actions.FirstOrDefault(x => x.StorageType == defaultStorage);
            byte[] photo = await downloader.DownloadKycPhoto(user.GetKycPhotoId());
            await context.RespondAsync(new KycPhotoDownloaded { Photo = photo, NationalId = user.GetKycRequestNationalId() });
        }
    }
}
