using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using System.ComponentModel.DataAnnotations;

namespace CXChange.Modules.Kyc.Infrastructure.DataModel
{
    internal class KycRequestDto
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string NationalId { get; set; }
        public DateOnly BirthDate { get; set; }
        public string KycPhotoId { get; set; }
        public KycRequestStatus KycRequestStatus { get; set; }
        public StorageTypeEnum StorageType { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

    }
}
