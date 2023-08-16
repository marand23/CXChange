namespace CXChange.API.Modules.UserManagment.Requests.UserLogin
{
    public record ConfirmLoginRequest
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string VerificationCode { get; set; }
    }
}
