namespace CXChange.API.Modules.UserManagment.Requests.Code
{
    public class SendPhoneNumberCodeRequest
    {
        public string PhoneNumber { get; set; }
        public string CodeType { get; set; }
        public string Token { get; set; }
    }
}
