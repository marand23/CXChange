using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Kyc.Domain.KycRequest.DomainEvents;
using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using CXChange.Modules.Kyc.Domain.User.ValueObjects;

namespace CXChange.Modules.Kyc.Domain.KycRequest
{
    public class KycRequest : Entity<Guid>
    {
        private KycRequest(Guid id, string name, string surname, string nationalId, DateOnly birthDate)
        {
            Id = id;
            FullName = new FullName(name, surname);
            BirthDate = birthDate;
            NationalId = new NationalId(nationalId);
            AddDomainEvent(new KycRequestInitiatedDomainEvent(this));
        }
        private KycRequest(KycRequestSnapShot snashot)
        {
            Id = snashot.Id;
            FullName = new FullName(snashot.Name, snashot.SurName);
            BirthDate = snashot.BirthDate;
            NationalId = new NationalId(snashot.NationalId);
            KycPhotoId = snashot.KycPhotoId;
            StorageType = snashot.StorageType;
            CreatedAt = snashot.CreatedAt;
        }

        private FullName FullName { get; set; }
        private NationalId NationalId { get; set; }
        private DateOnly BirthDate { get; set; }
        private string KycPhotoId { get; set; }
        private DateTimeOffset CreatedAt { get; }
        private KycRequestStatus Status { get; set; }
        private StorageTypeEnum StorageType;
        public static KycRequest CreateFrom(KycRequestSnapShot snapshot)
        {
            return new KycRequest(snapshot);

        }
        public static KycRequest CreateKycRequest(Guid id, string name, string surname, string nationalId, DateOnly birthDay)
        {

            return new KycRequest(id, name, surname, nationalId, birthDay);
        }
        public KycRequestSnapShot GetSnapShot()
        {
            return new KycRequestSnapShot
            {
                Id = Id,
                BirthDate = BirthDate,
                Name = FullName.GetSnapshot().Name,
                SurName = FullName.GetSnapshot().SurName,
                NationalId = NationalId.NationalNumber,
                KycPhotoId = KycPhotoId,
                KycRequestStatus = Status,
                StorageType = StorageType,
                CreatedAt = CreatedAt


            };
        }
        public void SetKycRequestPhotoId(string id)
        {
            KycPhotoId = id;
        }
        public void SetKycRequestStorageType(StorageTypeEnum storageType)
        {
            StorageType = storageType;
        }
        //after approval or rejection,
        //kycrequest state would change to stale
        //so scheduler could remove the row and the
        //image from storage provider
        public void MarkKycRequestAsStale()
        {
            Status = KycRequestStatus.Stale;
        }

    }
}
