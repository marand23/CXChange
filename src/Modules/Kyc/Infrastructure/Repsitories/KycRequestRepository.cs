using CXChange.Modules.Kyc.Domain.Interfaces.Repositories;
using CXChange.Modules.Kyc.Domain.KycRequest;
using CXChange.Modules.Kyc.Infrastructure.DataAccess;
using CXChange.Modules.Kyc.Infrastructure.DataModel;

namespace CXChange.Modules.Kyc.Infrastructure.Repsitories
{
    internal class KycRequestRepository : IKycRequestRepository
    {
        KycDbContext _context;
        public KycRequestRepository(KycDbContext context) { _context = context; }
        public async Task<KycRequest> FindByUserId(Guid UserId)
        {
            var kycRequestDataModel = await _context.KycRequests.FindAsync(UserId);
            if (kycRequestDataModel == null) { return null; }
            return KycRequest.CreateFrom(new KycRequestSnapShot
            {
                BirthDate = kycRequestDataModel.BirthDate,
                Id = kycRequestDataModel.UserId,
                Name = kycRequestDataModel.Name,
                NationalId = kycRequestDataModel.NationalId,
                KycPhotoId = kycRequestDataModel.KycPhotoId,
                SurName = kycRequestDataModel.SurName,
                KycRequestStatus = kycRequestDataModel.KycRequestStatus,
                StorageType = kycRequestDataModel.StorageType,
            });

        }
        public async Task AddAsync(KycRequest request)
        {
            KycRequestDto dto = new KycRequestDto();
            Map(request.GetSnapShot(), dto);
            await _context.KycRequests.AddAsync(dto);
        }
        public async Task Remove(Guid UserId)
        {
            var kycRequest = _context.KycRequests.Find(UserId);
            _context.KycRequests.Remove(kycRequest);
        }
        public async Task Update(KycRequest request)
        {

            var snapshot = request.GetSnapShot();
            KycRequestDto requestDto = await _context.KycRequests.FindAsync(snapshot.Id);
            Map(snapshot, requestDto);

        }
        private void Map(KycRequestSnapShot snapShot, KycRequestDto kycRequestData)
        {
            kycRequestData.BirthDate = snapShot.BirthDate;
            kycRequestData.SurName = snapShot.SurName;
            kycRequestData.Name = snapShot.Name;
            kycRequestData.NationalId = snapShot.NationalId;
            kycRequestData.UserId = snapShot.Id;
            kycRequestData.KycPhotoId = snapShot.KycPhotoId;
            kycRequestData.KycRequestStatus = snapShot.KycRequestStatus;
            kycRequestData.StorageType = snapShot.StorageType;
        }

    }
}
