using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User
{
    public class UserSnapShot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string NationalId { get; set; }
        public DateOnly BirthDate { get; set; }
        public LandLineStatus LandLineStatus { get; set; }
        public KycStatus KycStatus { get; set; }

    }
}
