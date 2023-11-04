using CXChange.Modules.Kyc.Domain.KycRequest.Enums;

namespace CXChange.Modules.Kyc.Domain.KycRequest
{
    public class KycRequestSnapShot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string NationalId { get; set; }
        public DateOnly BirthDate { get; set; }
        public string KycPhotoId { get; set; }
        public KycRequestStatus KycRequestStatus { get; set; }
        public StorageTypeEnum StorageType { get; set; }
    }
}
