namespace CXChange.API.Modules.Kyc.Requests
{
    public class KycRequest
    {
        public IFormFile Image { get; set; }
        public int BirthMonth { get; set; }
        public int BirthDay { get; set; }
        public int BirthYear { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string NationalId { get; set; }
        public Guid UserId { get; set; }

    }
}
