namespace CXChange.API.Modules.UserManagment.Requests
{
    public record UserConfirmationRequest
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
