namespace CXChange.API.Modules.UserManagment.Requests
{
    public class CodeVerifyRequest
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string CodeType { get; set; }
    }
}
