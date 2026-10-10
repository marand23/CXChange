namespace CXChange.API.Modules.UserManagment.Requests.Code
{
    public record SendEmailCodeRequest
    {
        public string Email { get; set; }
        public string CodeType { get; set; }
        public string Token { get; set; }
    }
}
