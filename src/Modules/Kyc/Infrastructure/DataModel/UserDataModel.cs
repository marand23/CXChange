using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Infrastructure.DataModel
{
    internal class UserDataModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? NationlId { get; set; }
        public DateOnly BirthDate { get; set; }
        public KycStatus kycStatus { get; set; }
        public int Version { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public LandLineStatus LandLineStatus { get; set; }
        public string? LandLine { get; set; }

    }
}
