namespace CXChange.API.Modules.UserManagment.Requests.Code
{
    public class ValidateAuthenticatorCodeRequest
    {
        public string CodeType { get; set; }
        public string Code { get; set; }

    }
}
