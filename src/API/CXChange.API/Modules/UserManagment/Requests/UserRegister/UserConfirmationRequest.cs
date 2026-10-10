namespace CXChange.API.Modules.UserManagment.Requests.UserRegister
{
    public record UserConfirmationRequest
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public string Token { get; set; }
    }
}
