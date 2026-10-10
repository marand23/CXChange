namespace CXChange.Modules.Kyc.Administrator.Application.Queries.KycRequestsGet
{
    public record KycRequestDto
    {
        public string UserId { get; set; }
        public string NationalId { get; set; }
        public string BirthDate { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string KycPhotoId { get; set; }

    }
}
