namespace CXChange.API.Modules.UserManagment.Requests.Code
{
    public class ValidateEmailCodeRequest : IValidateCode
    {
        public string CodeType { get; set; }
        public string Code { get; set; }

    }
}
