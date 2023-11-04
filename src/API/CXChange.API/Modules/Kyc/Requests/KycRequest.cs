namespace CXChange.API.Modules.Kyc.Requests
{
    public class KycRequest
    {
        public IFormFile Image { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string NationalId { get; set; }
        public Guid UserId { get; set; }

    }
}
