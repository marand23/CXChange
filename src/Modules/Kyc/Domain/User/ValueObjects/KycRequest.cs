using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User.ValueObjects
{
    public record KycRequest
    {
        private KycRequest() { }
        private KycRequest(string name, string surname, string nationalId, DateOnly birthDate)
        {

            FullName = new FullName(name, surname);
            BirthDate = birthDate;
            NationalId = new NationalId(nationalId);

        }
        public FullName FullName { get; init; }
        public NationalId NationalId { get; init; }
        public DateOnly BirthDate { get; init; }
        public string KycPhotoId { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public KycRequestStatus Status { get; init; }
        public StorageTypeEnum StorageType { get; init; }

        public static KycRequest CreateKycRequest(string name, string surname, string nationalId, DateOnly birthDay)
        {

            return new KycRequest(name, surname, nationalId, birthDay);
        }
    }
}
