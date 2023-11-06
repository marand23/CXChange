namespace CXChange.API.Modules.UserManagment.Requests.Code
{
    public class ValidatePhoneCodeRequest
    {
        public string CodeType { get; set; }

        public string Code { get; set; }
        public string PhoneNumber { get; set; }
    }
}
