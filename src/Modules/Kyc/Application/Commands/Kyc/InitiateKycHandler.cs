using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.Interfaces;
using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Domain.User.Enums;
using MassTransit;
using System.Globalization;

namespace CXChange.Modules.Kyc.Application.Commands.Kyc
{

    internal class InitiateKycHandler : IConsumer<InitiateKyc>
    {
        IUserRepository _repo;
        IEnumerable<IkycPhotoActions> _kycPhotoActions;
        IDomainEventDispatcher _dispatcher;


        public InitiateKycHandler(IEnumerable<IkycPhotoActions> kycPhotoActions, IUserRepository repo, IDomainEventDispatcher dispatcher)
        {
            _kycPhotoActions = kycPhotoActions;
            _repo = repo;
            _dispatcher = dispatcher;

            _dispatcher = dispatcher;
        }
        public async Task Consume(ConsumeContext<InitiateKyc> context)
        {
            var defaultStorate = StorageTypeEnum.GoogleDrive;
            var user = await _repo.FindUserById(context.Message.UserId);
            if (user == null)
            {

            }
            var message = context.Message;
            user.CreateKycRequest(message.Name, message.SurName, message.NationalId, new DateOnly(message.BirthDay.Year, message.BirthDay.Month, message.BirthDay.Day, new PersianCalendar()));
            var uploader = _kycPhotoActions.FirstOrDefault(x => x.StorageType == defaultStorate);
            var result = await uploader.UploadKycPhoto(context.Message.Image);
            user.SetKycRequestPhotoIdAndStore(result, defaultStorate);
            await _repo.UpdateUser(user);
            await context.RespondAsync(new KycInitiated { });
        }


    }
}
