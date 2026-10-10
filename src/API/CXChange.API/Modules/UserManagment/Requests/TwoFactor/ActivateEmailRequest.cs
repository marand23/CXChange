namespace CXChange.API.Modules.UserManagment.Requests.TwoFactor
{
    public record ConfrimAuthenticatorRequest
    {

        public string Email { get; set; }
        public string Token { get; set; }
        public string Code { get; set; }
    }
}
